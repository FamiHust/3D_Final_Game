using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [HideInInspector] public int gold;
    [SerializeField] private bool playDuel;

    void Start()
    {
        gold = PlayerPrefs.GetInt("gold", 100);
        UpdateGoldUI();
    }

    void Update()
    {
        if (playDuel == false)
        {
            UpdateGoldUI();
        }
        else
        {
            PlayerPrefs.SetInt("gold", gold);
        }
    }

    void UpdateGoldUI()
    {
        goldText.text = "GOLD: " + gold + "$";
    }

    public void BuyPackX1()
    {
        if (gold >= 10)
        {
            gold -= 10;
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.Save();
            SceneManager.LoadScene("OpenPack");
        }
    }

    public void BuyPackX10()
    {
        if (gold >= 100)
        {
            gold -= 100;
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.Save();
            SceneManager.LoadScene("OpenPack");
        }
    }
}
