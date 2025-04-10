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
        gold -= 10;
        if (gold <= 0) 
        {
            gold = 0;
            return;
        }
        // PlayerPrefs.SetInt("gold", gold);
        SceneManager.LoadScene("OpenPack");
    }

    public void BuyPackX10()
    {
        gold -= 100;
        if (gold <= 0) 
        {
            gold = 0;
            return;
        }
        
        // PlayerPrefs.SetInt("gold", gold);
        SceneManager.LoadScene("OpenPack");
    }
}
