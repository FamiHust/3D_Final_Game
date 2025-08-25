using UnityEngine;

public class AISummonTester : MonoBehaviour
{
    [Header("Test Settings")]
    [SerializeField] private KeyCode testSummonKey = KeyCode.S;
    [SerializeField] private KeyCode resetCardKey = KeyCode.R;
    [SerializeField] private GameObject testZone;
    
    [Header("Test Values")]
    [SerializeField] private float testStartScale = 0.1f;
    [SerializeField] private float testEndScale = 1.0f;
    [SerializeField] private float testDuration = 0.8f;
    
    private AISummonEffect summonEffect;
    private AICardToHand aiCard;
    
    void Start()
    {
        // Tìm component summon effect
        summonEffect = GetComponent<AISummonEffect>();
        
        // Tìm component card
        aiCard = GetComponent<AICardToHand>();
        
        if (summonEffect == null)
        {
            Debug.LogWarning("[AISummonTester] AISummonEffect component not found!");
        }
        else
        {
            Debug.Log("[AISummonTester] AISummonEffect component found and ready for testing");
        }
        
        if (aiCard == null)
        {
            Debug.LogWarning("[AISummonTester] No AICardToHand component found!");
        }
        
        // Tìm test zone nếu chưa có
        if (testZone == null)
        {
            var aiZone1 = GameObject.Find("AI_Zone_1");
            var aiZone2 = GameObject.Find("AI_Zone_2");
            var aiZone3 = GameObject.Find("AI_Zone_3");
            
            if (aiZone1 != null)
            {
                testZone = aiZone1;
                Debug.Log("[AISummonTester] Using AI_Zone_1 as test zone");
            }
            else if (aiZone2 != null)
            {
                testZone = aiZone2;
                Debug.Log("[AISummonTester] Using AI_Zone_2 as test zone");
            }
            else if (aiZone3 != null)
            {
                testZone = aiZone3;
                Debug.Log("[AISummonTester] Using AI_Zone_3 as test zone");
            }
            else
            {
                Debug.LogWarning("[AISummonTester] No AI zone found in scene!");
            }
        }
    }
    
    void Update()
    {
        // Test hiệu ứng summon
        if (Input.GetKeyDown(testSummonKey))
        {
            TestSummonEffect();
        }
        
        // Reset lá bài
        if (Input.GetKeyDown(resetCardKey))
        {
            ResetCard();
        }
    }
    
    void TestSummonEffect()
    {
        Debug.Log("[AISummonTester] Testing simple summon effect...");
        
        if (summonEffect == null)
        {
            Debug.LogError("[AISummonTester] No AISummonEffect component found!");
            return;
        }
        
        if (testZone == null)
        {
            Debug.LogError("[AISummonTester] No test zone found!");
            return;
        }
        
        // Cập nhật timing cho test
        summonEffect.SetSummonTiming(testDuration);
        summonEffect.SetStartScale(testStartScale);
        summonEffect.SetEndScale(testEndScale);
        
        // Bắt đầu hiệu ứng summon
        summonEffect.StartSummonEffect(testZone.transform, () => {
            Debug.Log("[AISummonTester] Simple summon effect completed!");
        });
        
        Debug.Log("[AISummonTester] Simple summon effect started!");
    }
    
    void ResetCard()
    {
        Debug.Log("[AISummonTester] Resetting card...");
        
        if (summonEffect != null)
        {
            summonEffect.ResetToOriginalState();
            Debug.Log("[AISummonTester] Card reset to original state");
        }
        
        // Reset các trạng thái nếu có
        if (aiCard != null)
        {
            aiCard.isSummoned = false;
            aiCard.summoningSickness = true;
            Debug.Log("[AISummonTester] AI card states reset");
        }
    }
    
    [ContextMenu("Test Summon Effect")]
    public void TestSummonEffectContext()
    {
        TestSummonEffect();
    }
    
    [ContextMenu("Reset Card")]
    public void ResetCardContext()
    {
        ResetCard();
    }
    
    [ContextMenu("Set Fast Summon")]
    public void SetFastSummon()
    {
        if (summonEffect != null)
        {
            summonEffect.SetSummonTiming(0.5f);
            summonEffect.SetStartScale(0.2f);
            summonEffect.SetEndScale(1.0f);
            Debug.Log("[AISummonTester] Set to fast summon mode");
        }
    }
    
    [ContextMenu("Set Slow Summon")]
    public void SetSlowSummon()
    {
        if (summonEffect != null)
        {
            summonEffect.SetSummonTiming(1.2f);
            summonEffect.SetStartScale(0.05f);
            summonEffect.SetEndScale(1.0f);
            Debug.Log("[AISummonTester] Set to slow summon mode");
        }
    }
    
    [ContextMenu("Set Normal Summon")]
    public void SetNormalSummon()
    {
        if (summonEffect != null)
        {
            summonEffect.SetSummonTiming(0.8f);
            summonEffect.SetStartScale(0.1f);
            summonEffect.SetEndScale(1.0f);
            Debug.Log("[AISummonTester] Set to normal summon mode");
        }
    }
    
    [ContextMenu("Set Ultra Fast Summon")]
    public void SetUltraFastSummon()
    {
        if (summonEffect != null)
        {
            summonEffect.SetSummonTiming(0.3f);
            summonEffect.SetStartScale(0.3f);
            summonEffect.SetEndScale(1.0f);
            Debug.Log("[AISummonTester] Set to ultra fast summon mode");
        }
    }
}
