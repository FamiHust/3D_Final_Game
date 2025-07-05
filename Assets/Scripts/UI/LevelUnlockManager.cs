// using UnityEngine;

// public class LevelUnlockManager : MonoBehaviour
// {
//     public static LevelUnlockManager Instance;

//     private void Awake()
//     {
//         if (Instance == null) Instance = this;
//     }

//     // Kiểm tra xem level đã mở chưa
//     public bool IsLevelUnlocked(AIType ai)
//     {
//         return PlayerPrefs.GetInt("LevelUnlocked_" + ai.ToString(), ai == AIType.ThuyTinh ? 1 : 0) == 1;
//         // Thủy Tinh là level đầu tiên nên mặc định mở
//     }

//     // Gọi khi thắng một level để mở khóa level tiếp theo
//     public void UnlockNextLevel(AIType currentLevel)
//     {
//         AIType nextLevel = GetNextLevel(currentLevel);
//         PlayerPrefs.SetInt("LevelUnlocked_" + nextLevel.ToString(), 1);
//         PlayerPrefs.Save();
//         Debug.Log("Unlocked " + nextLevel);
//     }

//     private AIType GetNextLevel(AIType current)
//     {
//         switch (current)
//         {
//             case AIType.ThuyTinh: return AIType.SonTinh;
//             case AIType.SonTinh: return AIType.YeuMa;
//             default: return current; // Không có level sau
//         }
//     }
// }
using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LevelUnlockManager : MonoBehaviour
{
    public static LevelUnlockManager Instance;

    private Dictionary<string, string> levelStates = new(); // levelName => "locked"/"unlocked"

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // giữ lại khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// Gọi sau khi đăng nhập thành công
    public void LoadLevelUnlocks(Action onSuccess = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            levelStates.Clear();
            if (result.Data != null)
            {
                foreach (var item in result.Data)
                    levelStates[item.Key] = item.Value.Value;
            }

            // Nếu chưa có dữ liệu, mặc định unlock Thủy Tinh
            if (!levelStates.ContainsKey("ThuyTinh"))
                levelStates["ThuyTinh"] = "unlocked";

            onSuccess?.Invoke();
        },
        error => Debug.LogError("LoadLevelUnlocks failed: " + error.GenerateErrorReport()));
    }

    public bool IsUnlocked(AIType type)
    {
        string key = type.ToString();
        return levelStates.ContainsKey(key) && levelStates[key] == "unlocked";
    }

    public void UnlockNextLevel(AIType defeated)
    {
        string nextLevel = GetNextLevel(defeated);
        if (string.IsNullOrEmpty(nextLevel)) return;

        levelStates[nextLevel] = "unlocked";

        var update = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { nextLevel, "unlocked" } }
        };
        PlayFabClientAPI.UpdateUserData(update, result =>
        {
            Debug.Log("Level " + nextLevel + " unlocked!");
        },
        error => Debug.LogError("UnlockNextLevel failed: " + error.GenerateErrorReport()));
    }

    private string GetNextLevel(AIType defeated)
    {
        return defeated switch
        {
            AIType.ThuyTinh => "SonTinh",
            AIType.SonTinh => "YeuMa",
            _ => null
        };
    }
}