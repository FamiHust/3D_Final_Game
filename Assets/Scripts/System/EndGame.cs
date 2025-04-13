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

    void Update()
    {
        if (PlayerHp.staticHp <= 0)
        {
            GameOver();
        }

        if (EnemyHp.staticHp <= 0)
        {
            Victory();
            if (gotMoney == false)
            {
                money.GetComponent<Shop>().gold += goldReceived;
                gotMoney = true;               
            }
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
        SceneManager.LoadScene("AdventureTime");
        Time.timeScale = 1;
    }
}
