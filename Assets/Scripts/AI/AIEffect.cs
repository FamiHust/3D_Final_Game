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
        // anim.SetTrigger("isHurted");
        EffectManager.Instance.PlayAttackEffectAtUI(uiTransform);
    }
}
