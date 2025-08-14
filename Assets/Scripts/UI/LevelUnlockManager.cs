using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LevelUnlockManager : MonoBehaviour
{
    public static LevelUnlockManager Instance;

    private Dictionary<string, string> levelStates = new(); 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

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