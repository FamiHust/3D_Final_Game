using UnityEngine;

public class SimpleHealthTester : MonoBehaviour
{
    [Header("Test Settings")]
    [SerializeField] private KeyCode testPlayerDamageKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode testPlayerHealKey = KeyCode.Alpha2;
    [SerializeField] private KeyCode testEnemyDamageKey = KeyCode.Alpha3;
    [SerializeField] private KeyCode testEnemyHealKey = KeyCode.Alpha4;
    
    [Header("Test Parameters")]
    [SerializeField] private float testDamageAmount = 5f;
    [SerializeField] private float testHealAmount = 8f;
    
    void Update()
    {
        if (Input.GetKeyDown(testPlayerDamageKey))
        {
            PlayerHp.TakeDamage(testDamageAmount);
        }
        
        if (Input.GetKeyDown(testPlayerHealKey))
        {
            PlayerHp.Heal(testHealAmount);
        }
        
        if (Input.GetKeyDown(testEnemyDamageKey))
        {
            EnemyHp.TakeDamage(testDamageAmount);
        }
        
        if (Input.GetKeyDown(testEnemyHealKey))
        {
            EnemyHp.Heal(testHealAmount);
        }
    }
    
    [ContextMenu("Test Player Damage")]
    public void TestPlayerDamage()
    {
        PlayerHp.TakeDamage(testDamageAmount);
    }
    
    [ContextMenu("Test Player Heal")]
    public void TestPlayerHeal()
    {
        PlayerHp.Heal(testHealAmount);
    }
    
    [ContextMenu("Test Enemy Damage")]
    public void TestEnemyDamage()
    {
        EnemyHp.TakeDamage(testDamageAmount);
    }
    
    [ContextMenu("Test Enemy Heal")]
    public void TestEnemyHeal()
    {
        EnemyHp.Heal(testHealAmount);
    }
    
    [ContextMenu("Reset Player Health")]
    public void ResetPlayerHealth()
    {
        PlayerHp.SetHealth(20f);
    }
    
    [ContextMenu("Reset Enemy Health")]
    public void ResetEnemyHealth()
    {
        EnemyHp.SetHealth(20f);
    }
}
