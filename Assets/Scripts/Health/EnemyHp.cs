using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    public static float staticHp; 

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    
    [Header("Model Reference")]
    [SerializeField] private Transform enemyModel; // Kéo model AI vào đây
    
    // Public property để truy cập enemyModel từ bên ngoài
    public Transform EnemyModel => enemyModel;

    [SerializeField] private float maxHp = 20f;
    [SerializeField] private float startHp = 20f;
    private float currentDisplayHp;
    private float previousHp;

    void Start()
    {
        staticHp = Mathf.Clamp(startHp, 0, maxHp);
        currentDisplayHp = staticHp;
        previousHp = staticHp;

        healthSlider.maxValue = maxHp;
        healthSlider.value = currentDisplayHp;
    }

    void Update()
    {
        if (staticHp <= 0)
            staticHp = 0;
        float hp = Mathf.Clamp(staticHp, 0, maxHp);

        currentDisplayHp = Mathf.Lerp(currentDisplayHp, hp, Time.deltaTime * 10f);
        healthSlider.value = currentDisplayHp;

        hpText.text = Mathf.RoundToInt(hp) + "/" + maxHp;
        
        CheckHealthChange(hp);
        previousHp = hp;
    }
    
    private void CheckHealthChange(float currentHp)
    {
        if (currentHp != previousHp)
        {
            float healthDifference = currentHp - previousHp;
            
            if (healthDifference < 0)
            {
                ShowDamageEffect();
            }
            else if (healthDifference > 0)
            {
                ShowHealEffect();
            }
        }
    }
    
    private void ShowDamageEffect()
    {
        if (SimpleParticleManager.Instance != null)
        {
            // Hiển thị hiệu ứng ở vị trí của model AI, nếu không có thì ở vị trí hiện tại
            Vector3 effectPosition = enemyModel != null ? enemyModel.position : transform.position;
            SimpleParticleManager.Instance.ShowDamageEffect(effectPosition);
        }
    }
    
    private void ShowHealEffect()
    {
        if (SimpleParticleManager.Instance != null)
        {
            // Hiển thị hiệu ứng ở vị trí của model AI, nếu không có thì ở vị trí hiện tại
            Vector3 effectPosition = enemyModel != null ? enemyModel.position : transform.position;
            SimpleParticleManager.Instance.ShowHealEffect(effectPosition);
        }
    }
    
    public static void TakeDamage(float damage)
    {
        staticHp -= damage;
    }
    
    public static void Heal(float healAmount)
    {
        staticHp += healAmount;
    }
    
    public static void SetHealth(float newHealth)
    {
        staticHp = newHealth;
    }
}
