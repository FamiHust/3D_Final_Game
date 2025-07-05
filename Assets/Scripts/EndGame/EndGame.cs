using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    [SerializeField] private GameObject money;
    [SerializeField] private bool gotMoney;
    [SerializeField] private int goldReceived;
    [SerializeField] private int diamondReceived;
    private bool hasAddedScore = false;

    public AIType currentLevelAI; 

    private bool isGameEnded = false;

    void Update()
    {
        if (isGameEnded) return;

        if (PlayerHp.staticHp <= 0)
        {
            isGameEnded = true;
            GameOver();
        }

        if (EnemyHp.staticHp <= 0)
        {
            Victory();

            if (!gotMoney)
            {
                AI.currentLevel = currentLevelAI;

                PlayfabGoldManager.Instance.ChangeGold(goldReceived);
                PlayfabGoldManager.Instance.ChangeDiamond(diamondReceived);

                int turnsUsed = FindObjectOfType<TurnSystem>().playerTurnCount;
                int maxScore = 100;
                int scoreToAdd = Mathf.Max(0, maxScore - turnsUsed * 2);
                PlayerScoreManager.Instance.AddScore(scoreToAdd);

                gotMoney = true;
                hasAddedScore = true;

                LevelUnlockManager.Instance.UnlockNextLevel(currentLevelAI);
            }

            isGameEnded = true; // Đảm bảo không gọi lại Victory nữa
        }
    }


    public void Victory()
    {
        StartCoroutine(DelayBeforeVictory());
    }

    private IEnumerator DelayBeforeVictory()
    {
        WinPanel.SetActive(true);
        SoundManager.PlaySound(SoundType.Victory);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;

    }

    public void GameOver()
    {
        StartCoroutine(DelayBeforeGameOver());
    }

    private IEnumerator DelayBeforeGameOver()
    {
        LosePanel.SetActive(true);
        SoundManager.PlaySound(SoundType.Defeated);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;

    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("AdventureTime");
        Time.timeScale = 1;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
