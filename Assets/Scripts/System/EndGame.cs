using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;

    void Update()
    {
        if (PlayerHp.staticHp <= 0)
        {
            GameOver();
        }

        if (EnemyHp.staticHp <= 0)
        {
            Victory();
        }
    }

    public void Victory()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        LosePanel.SetActive(true);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AdventureTime");
    }
}
