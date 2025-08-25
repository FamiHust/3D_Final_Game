using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AISummonEffect : MonoBehaviour
{
    [Header("Summon Effect Settings")]
    [SerializeField] private float summonDuration = 0.8f;
    [SerializeField] private float startScale = 0.1f;
    [SerializeField] private float endScale = 1.0f;
    
    [Header("Animation Easing")]
    [SerializeField] private Ease scaleEase = Ease.OutBack;
    
    [Header("Particle Effects")]
    [SerializeField] private GameObject summonParticles;
    
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Transform originalParent;
    
    private void Awake()
    {
        // Lưu trạng thái ban đầu
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
        originalParent = transform.parent;
    }
    
    /// <summary>
    /// Bắt đầu hiệu ứng summon đơn giản với scale ra dần
    /// </summary>
    /// <param name="targetZone">Zone đích để đặt lá bài</param>
    /// <param name="onComplete">Callback khi hoàn thành</param>
    public void StartSummonEffect(Transform targetZone, System.Action onComplete = null)
    {
        StartCoroutine(SimpleSummonSequence(targetZone, onComplete));
    }
    
    private IEnumerator SimpleSummonSequence(Transform targetZone, System.Action onComplete)
    {
        Debug.Log($"[AISummonEffect] Starting simple summon effect for {gameObject.name} to {targetZone.name}");
        
        // 1. Chuẩn bị - đặt lá bài vào zone với scale nhỏ
        PrepareSimpleSummon(targetZone);
        
        // 2. Hiệu ứng scale ra dần
        yield return StartCoroutine(SimpleScaleEffect());
        
        // 3. Hoàn thành
        Debug.Log($"[AISummonEffect] Simple summon effect completed for {gameObject.name}");
        onComplete?.Invoke();
    }
    
    private void PrepareSimpleSummon(Transform targetZone)
    {
        Debug.Log($"[AISummonEffect] Preparing simple summon effect");
        
        // Đặt lá bài vào zone
        transform.SetParent(targetZone);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one * startScale;
        
        // Hiệu ứng particle nếu có
        if (summonParticles != null)
        {
            Instantiate(summonParticles, targetZone.position, Quaternion.identity);
        }
    }
    
    private IEnumerator SimpleScaleEffect()
    {
        Debug.Log($"[AISummonEffect] Starting simple scale effect");
        
        // Tạo sequence cho hiệu ứng scale đơn giản
        Sequence scaleSequence = DOTween.Sequence();
        
        // Scale ra dần với easing mượt mà
        scaleSequence.Append(transform.DOScale(Vector3.one * endScale, summonDuration)
            .SetEase(scaleEase));
        
        yield return scaleSequence.WaitForCompletion();
        
        Debug.Log($"[AISummonEffect] Simple scale effect completed");
    }
    
    /// <summary>
    /// Reset lá bài về trạng thái ban đầu (dùng để test)
    /// </summary>
    public void ResetToOriginalState()
    {
        if (originalParent != null)
        {
            transform.SetParent(originalParent);
            transform.localPosition = originalPosition;
            transform.localScale = originalScale;
            transform.localRotation = originalRotation;
        }
    }
    
    /// <summary>
    /// Thay đổi thời gian hiệu ứng trong runtime
    /// </summary>
    public void SetSummonTiming(float duration)
    {
        this.summonDuration = duration;
        Debug.Log($"[AISummonEffect] Timing updated: Duration={duration}s");
    }
    
    /// <summary>
    /// Lấy thông tin về timing hiện tại
    /// </summary>
    public string GetTimingInfo()
    {
        return $"Simple Summon Effect Timing: Duration={summonDuration}s";
    }
    
    /// <summary>
    /// Thay đổi scale ban đầu
    /// </summary>
    public void SetStartScale(float scale)
    {
        this.startScale = scale;
        Debug.Log($"[AISummonEffect] Start scale updated: {scale}");
    }
    
    /// <summary>
    /// Thay đổi scale cuối cùng
    /// </summary>
    public void SetEndScale(float scale)
    {
        this.endScale = scale;
        Debug.Log($"[AISummonEffect] End scale updated: {scale}");
    }
    
    /// <summary>
    /// Test hiệu ứng summon (dùng để test trong Editor)
    /// </summary>
    [ContextMenu("Test Summon Effect")]
    public void TestSummonEffect()
    {
        // Tìm một zone để test
        var testZone = GameObject.Find("AI_Zone_1");
        if (testZone != null)
        {
            StartSummonEffect(testZone.transform, () => {
                Debug.Log("[AISummonEffect] Test summon effect completed!");
            });
        }
        else
        {
            Debug.LogWarning("[AISummonEffect] No test zone found!");
        }
    }
}
