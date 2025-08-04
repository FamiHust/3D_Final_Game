// using UnityEngine;

// public class AIEffect : MonoBehaviour
// {
//     private Animator anim;
//     public RectTransform uiTransform;
//     public Camera uiCamera;

//     private void Awake()
//     {
//         anim = GetComponent<Animator>();
//         uiTransform = GetComponent<RectTransform>();

//         // Tự động tìm camera từ Canvas cha
//         Canvas canvas = GetComponentInParent<Canvas>();
//         if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
//         {
//             uiCamera = canvas.worldCamera;
//         }

//         if (uiCamera == null)
//         {
//             Debug.LogError("Không tìm thấy UI Camera! Hãy gán camera cho Canvas.");
//         }
//     }

//     public void PlayHurtAnimation()
//     {
//         if (anim != null)
//         {
//             // anim.SetTrigger("isHurrted");
//             EffectManager.Instance.PlayAttackEffectAtUI(uiTransform, uiCamera);
//             Debug.Log("Animation acted");
//         }
//     }

//     // Bạn có thể bổ sung các hàm hiệu ứng khác tại đây, ví dụ:
//     // public void PlayHealAnimation() { ... }
//     // public void PlaySummonAnimation() { ... }
// }
using UnityEngine;

public class AIEffect : MonoBehaviour
{
    private Animator anim;
    private RectTransform uiTransform;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        uiTransform = GetComponent<RectTransform>();
    }

    public void PlayHurtAnimation()
    {
        if (anim != null)
        {
            // anim.SetTrigger("isHurted");
            EffectManager.Instance.PlayAttackEffectAtUI(uiTransform);
            Debug.Log("Animation acted");
        }
    }

    // Thêm các hiệu ứng khác nếu cần ở đây
}
