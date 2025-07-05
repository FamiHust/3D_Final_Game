using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance;

    [Header("Score Display")]
    public TextMeshProUGUI scoreText; // Gán TextMeshPro từ Inspector

    public int CurrentScore { get; private set; } = 0;

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
            return;
        }

        LoadScoreFromPlayFab();
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        UpdateScoreUI();
        SaveScoreToPlayFab();
    }

    public void SetScore(int value)
    {
        CurrentScore = value;
        UpdateScoreUI();
        SaveScoreToPlayFab();
    }

    public void LoadScoreFromPlayFab()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("Score"))
            {
                int.TryParse(result.Data["Score"].Value, out int loadedScore);
                CurrentScore = loadedScore;
            }
            else
            {
                CurrentScore = 0;
            }

            UpdateScoreUI();
            Debug.Log($"[PlayFab] Score loaded: {CurrentScore}");
        },
        error =>
        {
            Debug.LogError($"[PlayFab] Failed to load score: {error.ErrorMessage}");
        });
    }

    public void SaveScoreToPlayFab()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Score", CurrentScore.ToString() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log($"[PlayFab] Score saved: {CurrentScore}"),
            error => Debug.LogError($"[PlayFab] Failed to save score: {error.ErrorMessage}")
        );
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {CurrentScore}";
        }
    }
}
