using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public GameObject EndGame_Panel;

    void Start()
    {
        EndGame_Panel.SetActive(false);
    }

    void Update()
    {
        if (PlayerHp.staticHp <= 0)
        {
            EndGame_Panel.SetActive(true);
            victoryText.text = "DEFEATED";
        }

        if (EnemyHp.staticHp <= 0)
        {
            EndGame_Panel.SetActive(true);
            victoryText.text = "VICTORY";
        }
    }

}
