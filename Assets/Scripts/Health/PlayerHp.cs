using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    public static float staticHp; 

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    
    [Header("Model Reference")]
    [SerializeField] private Transform playerModel; // Kéo model Player vào đây
    
    // Public property để truy cập playerModel từ bên ngoài
    public Transform PlayerModel => playerModel;

    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float maxHp = 20f;
    [SerializeField] private float startHp = 20f;
    private float displayedHp;
    private float previousHp;

    void Start()
    {
        staticHp = Mathf.Clamp(startHp, 0, maxHp);
        displayedHp = staticHp;
        previousHp = staticHp;

        healthSlider.maxValue = maxHp;
        healthSlider.value = displayedHp;
    }

    void Update()
    {
        if (staticHp <= 0)
            staticHp = 0;
        float clampedHp = Mathf.Clamp(staticHp, 0, maxHp);

        displayedHp = Mathf.Lerp(displayedHp, clampedHp, Time.deltaTime * lerpSpeed);

        healthSlider.value = displayedHp;
        hpText.text = Mathf.CeilToInt(clampedHp) + "/" + maxHp;
        
        CheckHealthChange(clampedHp);
        previousHp = clampedHp;
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
            // Hiển thị hiệu ứng ở vị trí của model Player, nếu không có thì ở vị trí hiện tại
            Vector3 effectPosition = playerModel != null ? playerModel.position : transform.position;
            SimpleParticleManager.Instance.ShowDamageEffect(effectPosition);
        }
    }
    
    private void ShowHealEffect()
    {
        if (SimpleParticleManager.Instance != null)
        {
            // Hiển thị hiệu ứng ở vị trí của model Player, nếu không có thì ở vị trí hiện tại
            Vector3 effectPosition = playerModel != null ? playerModel.position : transform.position;
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
