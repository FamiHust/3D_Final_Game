using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    public static float maxHp;
    public static float staticHp;
    public float hp;
    public Image Health;
    public TextMeshProUGUI hpText;

    void Start()
    {
        maxHp = 20;
        staticHp = 10;
    }

    void Update()
    {
        hp = staticHp;
        Health.fillAmount = hp / maxHp;

        if (hp >= maxHp)
        {
            hp = maxHp;
        }

        if (hp <= 0)
        {
            hp = 0;
        }

        hpText.text = hp + "/" + maxHp;
    }
    

}
