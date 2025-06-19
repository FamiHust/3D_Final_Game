using UnityEngine;

public class AIEffect : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayHurtAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("isHurrted");
            Debug.Log("Animation acted");
        }
    }

    // Bạn có thể bổ sung các hàm hiệu ứng khác tại đây, ví dụ:
    // public void PlayHealAnimation() { ... }
    // public void PlaySummonAnimation() { ... }
}
