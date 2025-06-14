// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class PlayerHp : MonoBehaviour
// {
//     public static float maxHp;
//     public static float staticHp;
//     public float hp;
//     private float displayedHp; // máu đang hiển thị (được lerp tới)

//     [SerializeField] private Slider healthSlider;
//     [SerializeField] private TextMeshProUGUI hpText;
//     [SerializeField] private float lerpSpeed = 5f; // tốc độ lerp, có thể điều chỉnh trong Inspector

//     void Start()
//     {
//         maxHp = 20;
//         staticHp = 20;
//         displayedHp = staticHp;

//         healthSlider.maxValue = maxHp;
//         healthSlider.value = displayedHp;
//     }

//     void Update()
//     {
//         hp = Mathf.Clamp(staticHp, 0, maxHp);

//         // Lerp giá trị đang hiển thị dần về giá trị thật
//         displayedHp = Mathf.Lerp(displayedHp, hp, Time.deltaTime * lerpSpeed);

//         // Cập nhật thanh máu và text
//         healthSlider.value = displayedHp;
//         hpText.text = Mathf.CeilToInt(hp) + "/" + maxHp;
//     }
// }
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

    public static float staticHp; // Dùng để truy cập máu từ nơi khác
    private float displayedHp;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private float lerpSpeed = 5f;

    void Start()
    {
        // Gán máu ban đầu
        staticHp = Mathf.Clamp(startHp, 0, maxHp);
        displayedHp = staticHp;

        // Cập nhật UI
        healthSlider.maxValue = maxHp;
        healthSlider.value = displayedHp;
    }

    void Update()
    {
        float clampedHp = Mathf.Clamp(staticHp, 0, maxHp);

        // Lerp giá trị đang hiển thị dần về giá trị thật
        displayedHp = Mathf.Lerp(displayedHp, clampedHp, Time.deltaTime * lerpSpeed);

        // Cập nhật thanh máu và text
        healthSlider.value = displayedHp;
        hpText.text = Mathf.CeilToInt(clampedHp) + "/" + maxHp;
    }
}
