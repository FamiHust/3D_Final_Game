// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class EnemyHp : MonoBehaviour
// {
//     public static float maxHp;
//     public static float staticHp;
//     public float hp;

//     [SerializeField] private Slider healthSlider;
//     [SerializeField] private TextMeshProUGUI hpText;

//     private float currentDisplayHp; // giá trị hiển thị mượt

//     void Start()
//     {
//         maxHp = 20;
//         staticHp = 20;

//         healthSlider.maxValue = maxHp;
//         currentDisplayHp = staticHp;
//     }

//     void Update()
//     {
//         hp = Mathf.Clamp(staticHp, 0, maxHp); // đảm bảo nằm trong khoảng cho phép

//         // Lerp để làm mượt hiệu ứng
//         currentDisplayHp = Mathf.Lerp(currentDisplayHp, hp, Time.deltaTime * 10f);
//         healthSlider.value = currentDisplayHp;

//         // Cập nhật text theo giá trị thật, không phải giá trị mượt
//         hpText.text = Mathf.RoundToInt(hp) + "/" + maxHp;
//     }
// }
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

    public static float staticHp; // dùng để truy cập từ các script khác (ví dụ khi enemy bị tấn công)

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
        float hp = Mathf.Clamp(staticHp, 0, maxHp);

        // Làm mượt hiệu ứng hiển thị
        currentDisplayHp = Mathf.Lerp(currentDisplayHp, hp, Time.deltaTime * 10f);
        healthSlider.value = currentDisplayHp;

        // Cập nhật text theo giá trị thực tế
        hpText.text = Mathf.RoundToInt(hp) + "/" + maxHp;
    }
}
