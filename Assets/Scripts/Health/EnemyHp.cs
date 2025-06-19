using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    [Header("Cài đặt máu")]
    [SerializeField] private float maxHp = 20f;
    [SerializeField] private float startHp = 20f;

    public static float staticHp; 

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    private float currentDisplayHp;

    void Start()
    {
        staticHp = Mathf.Clamp(startHp, 0, maxHp);
        currentDisplayHp = staticHp;

        healthSlider.maxValue = maxHp;
        healthSlider.value = currentDisplayHp;
    }

    void Update()
    {
        if (staticHp <= 0)
            staticHp = 0;
        float hp = Mathf.Clamp(staticHp, 0, maxHp);

        currentDisplayHp = Mathf.Lerp(currentDisplayHp, hp, Time.deltaTime * 10f);
        healthSlider.value = currentDisplayHp;

        hpText.text = Mathf.RoundToInt(hp) + "/" + maxHp;
    }
}
