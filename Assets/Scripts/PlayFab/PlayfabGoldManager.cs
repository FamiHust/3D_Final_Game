using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabGoldManager : MonoBehaviour // Rename to PlayfabCurrencyManager if you want
{
    public static PlayfabGoldManager Instance;

    public int Gold { get; private set; } = 0;
    public int Diamond { get; private set; } = 0;

    public event Action OnGoldChanged;
    public event Action OnDiamondChanged;

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

    public void LoadGoldFromPlayfab(Action onComplete = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null)
            {
                if (result.Data.ContainsKey("gold"))
                    Gold = int.Parse(result.Data["gold"].Value);
                else
                    Gold = 100000;

                if (result.Data.ContainsKey("diamond"))
                    Diamond = int.Parse(result.Data["diamond"].Value);
                else
                    Diamond = 50;

                SaveAllToPlayfab(); // Đảm bảo lưu nếu dữ liệu chưa có
            }

            PlayerPrefs.SetInt("gold", Gold);
            PlayerPrefs.SetInt("diamond", Diamond);
            onComplete?.Invoke();
        },
        error =>
        {
            Debug.LogError("Error loading data: " + error.GenerateErrorReport());
            Gold = 100000;
            Diamond = 50;
            onComplete?.Invoke();
        });
    }

    public void SaveAllToPlayfab()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "gold", Gold.ToString() },
                { "diamond", Diamond.ToString() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Currency saved to Playfab.");
            OnGoldChanged?.Invoke();
            OnDiamondChanged?.Invoke();
        },
        error =>
        {
            Debug.LogError("Error saving currency: " + error.GenerateErrorReport());
        });
    }

    public void ChangeGold(int amount)
    {
        Gold += amount;
        PlayerPrefs.SetInt("gold", Gold);
        SaveAllToPlayfab();
    }

    public void ChangeDiamond(int amount)
    {
        Diamond += amount;
        PlayerPrefs.SetInt("diamond", Diamond);
        SaveAllToPlayfab();
    }
}


