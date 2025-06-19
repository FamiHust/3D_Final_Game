using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [Header("Cài đặt máu")]
    [SerializeField] private float maxHp = 20f;
    [SerializeField] private float startHp = 20f;

    public static float staticHp; 
    private float displayedHp;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private float lerpSpeed = 5f;

    void Start()
    {
        staticHp = Mathf.Clamp(startHp, 0, maxHp);
        displayedHp = staticHp;

        healthSlider.maxValue = maxHp;
        healthSlider.value = displayedHp;
    }

    void Update()
    {
        if (staticHp <= 0)
            staticHp = 0;
        float clampedHp = Mathf.Clamp(staticHp, 0, maxHp);

        displayedHp = Mathf.Lerp(displayedHp, clampedHp, Time.deltaTime * lerpSpeed);

        healthSlider.value = displayedHp;
        hpText.text = Mathf.CeilToInt(clampedHp) + "/" + maxHp;
    }
}
