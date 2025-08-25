using UnityEngine;

public class DamagePopupTester : MonoBehaviour
{
    [Header("Test Settings")]
    [SerializeField] private KeyCode testPlayerDamageKey = KeyCode.P;
    [SerializeField] private KeyCode testEnemyDamageKey = KeyCode.E;
    [SerializeField] private KeyCode testFontChangeKey = KeyCode.F;
    
    [Header("Test Parameters")]
    [SerializeField] private int testDamage = 5;
    [SerializeField] private Vector3 testOffset = Vector3.up * 2f;
    
    void Update()
    {
        // Test popup sát thương cho player card
        if (Input.GetKeyDown(testPlayerDamageKey))
        {
            if (DamagePopupManager.Instance != null)
            {
                Vector3 testPosition = transform.position + testOffset;
                DamagePopupManager.Instance.ShowPlayerDamagePopup(testPosition, testDamage);
                Debug.Log($"Test Player Damage Popup: {testDamage} damage at {testPosition}");
            }
            else
            {
                Debug.LogWarning("DamagePopupManager not found!");
            }
        }
        
        // Test popup sát thương cho enemy card
        if (Input.GetKeyDown(testEnemyDamageKey))
        {
            if (DamagePopupManager.Instance != null)
            {
                Vector3 testPosition = transform.position + testOffset;
                DamagePopupManager.Instance.ShowEnemyDamagePopup(testPosition, testDamage);
                Debug.Log($"Test Enemy Damage Popup: {testDamage} damage at {testPosition}");
            }
            else
            {
                Debug.LogWarning("DamagePopupManager not found!");
            }
        }
        
        // Test thay đổi font
        if (Input.GetKeyDown(testFontChangeKey))
        {
            if (DamagePopupManager.Instance != null)
            {
                DamagePopupManager.Instance.LoadFontFromResources("Fonts & Materials/Changa-Medium_Underlay SDF");
                
                Vector3 testPosition = transform.position + testOffset;
                DamagePopupManager.Instance.ShowPlayerDamagePopup(testPosition, testDamage);
                Debug.Log($"Test Font Change: {testDamage} damage at {testPosition} with Changa font");
            }
            else
            {
                Debug.LogWarning("DamagePopupManager not found!");
            }
        }
    }
    
    // Test methods có thể gọi từ Inspector
    [ContextMenu("Test Player Damage Popup")]
    public void TestPlayerDamagePopup()
    {
        if (DamagePopupManager.Instance != null)
        {
            Vector3 testPosition = transform.position + testOffset;
            DamagePopupManager.Instance.ShowPlayerDamagePopup(testPosition, testDamage);
        }
    }
    
    [ContextMenu("Test Enemy Damage Popup")]
    public void TestEnemyDamagePopup()
    {
        if (DamagePopupManager.Instance != null)
        {
            Vector3 testPosition = transform.position + testOffset;
            DamagePopupManager.Instance.ShowEnemyDamagePopup(testPosition, testDamage);
        }
    }
    
    [ContextMenu("Test Font Change")]
    public void TestFontChange()
    {
        if (DamagePopupManager.Instance != null)
        {
            DamagePopupManager.Instance.LoadFontFromResources("Fonts & Materials/Changa-Medium_Underlay SDF");
            
            Vector3 testPosition = transform.position + testOffset;
            DamagePopupManager.Instance.ShowPlayerDamagePopup(testPosition, testDamage);
        }
    }
}
