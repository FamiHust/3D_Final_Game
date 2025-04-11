using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public int gold;
    // Start is called before the first frame update
    void Start()
    {
        // gold = PlayerPrefs.GetInt("gold", 100);
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "GOLD: " + gold + "$";
    }

    public void BuyPackX1()
    {
        if (gold >= 10) 
        {
            gold -= 10;
            // PlayerPrefs.SetInt("gold", gold);

            SceneManager.LoadScene("OpenPack");
        }

        if (gold <= 0)
        {
            gold = 0;
            return;
        }
    }

    public void BuyPackX10()
    {
        if (gold >= 100)
        {
            gold -= 100;
            // PlayerPrefs.SetInt("gold", gold);

            SceneManager.LoadScene("OpenPack");
        }

        if (gold <= 0)
        {
            gold = 0;
            return;
        }
    }
}
