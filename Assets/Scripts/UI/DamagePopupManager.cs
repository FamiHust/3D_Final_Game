using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance;
    
    [Header("Popup Settings")]
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private float popupDuration = 1.5f;
    [SerializeField] private float popupDistance = 1f;
    [SerializeField] private Color playerDamageColor = Color.red;
    [SerializeField] private Color enemyDamageColor = Color.red;
    [SerializeField] private int fontSize = 48;
    [SerializeField] private float popupHeightOffset = 1.5f;
    [SerializeField] private float popupScale = 0.15f;
    [SerializeField] private TMP_FontAsset customFont;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Hiển thị popup sát thương cho player card
    public void ShowPlayerDamagePopup(Vector3 position, int damage)
    {
        ShowDamagePopup(position, damage, playerDamageColor, false);
    }
    
    // Hiển thị popup sát thương cho AI card
    public void ShowEnemyDamagePopup(Vector3 position, int damage)
    {
        ShowDamagePopup(position, damage, enemyDamageColor, true);
    }
    
    // Hiển thị popup sát thương chung
    private void ShowDamagePopup(Vector3 position, int damage, Color color, bool isEnemy)
    {
        GameObject damagePopup = new GameObject("DamagePopup");
        damagePopup.transform.position = position + Vector3.up * popupHeightOffset;
        
        TextMeshProUGUI damageText = damagePopup.AddComponent<TextMeshProUGUI>();
        damageText.text = "-" + damage.ToString();
        damageText.fontSize = fontSize;
        damageText.color = color;
        
        TMP_FontAsset fontToUse = customFont;
        if (fontToUse == null)
        {
            fontToUse = Resources.Load<TMPro.TMP_FontAsset>("Fonts & Materials/Changa-Medium_Underlay SDF");
        }
        
        if (fontToUse != null)
        {
            damageText.font = fontToUse;
        }
        
        damageText.alignment = TextAlignmentOptions.Center;
        
        Canvas canvas = damagePopup.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        canvas.sortingOrder = 100;
        
        CanvasScaler scaler = damagePopup.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        scaler.scaleFactor = 1f;
        
        damagePopup.AddComponent<GraphicRaycaster>();
        
        damagePopup.transform.localScale = Vector3.one * popupScale;
        
        StartCoroutine(AnimateDamagePopup(damagePopup, damage, color));
        StartCoroutine(ForceFontSize(damageText));
    }
    
    private IEnumerator ForceFontSize(TextMeshProUGUI text)
    {
        yield return null;
        text.fontSize = fontSize;
        yield return null;
        
        if (text.fontSize != fontSize)
        {
            text.fontSize = fontSize;
        }
    }
    
    private IEnumerator AnimateDamagePopup(GameObject popup, int damage, Color color)
    {
        Vector3 startPos = popup.transform.position;
        Vector3 endPos = startPos + Vector3.up * popupDistance;
        
        float duration = popupDuration;
        float elapsed = 0f;
        
        TextMeshProUGUI text = popup.GetComponent<TextMeshProUGUI>();
        Color startColor = color;
        startColor.a = 0f;
        text.color = startColor;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            popup.transform.position = Vector3.Lerp(startPos, endPos, progress);
            
            if (progress < 0.3f)
            {
                float alpha = progress / 0.3f;
                text.color = new Color(color.r, color.g, color.b, alpha);
            }
            else if (progress > 0.7f)
            {
                float alpha = 1f - ((progress - 0.7f) / 0.3f);
                text.color = new Color(color.r, color.g, color.b, alpha);
            }
            
            yield return null;
        }
        
        Destroy(popup);
    }
    
    // Method để thay đổi font động
    public void SetCustomFont(TMP_FontAsset newFont)
    {
        customFont = newFont;
    }
    
    // Method để load font từ Resources
    public void LoadFontFromResources(string fontPath)
    {
        TMP_FontAsset loadedFont = Resources.Load<TMPro.TMP_FontAsset>(fontPath);
        if (loadedFont != null)
        {
            customFont = loadedFont;
        }
    }
    
    // Method để reset về font mặc định
    public void ResetToDefaultFont()
    {
        customFont = null;
    }
    
    // Reset popup về cài đặt mặc định
    public void ResetToDefaultSettings()
    {
        fontSize = 48;
        popupHeightOffset = 1.5f;
        popupScale = 0.15f;
        popupDuration = 1.5f;
        popupDistance = 1f;
        playerDamageColor = Color.red;
        enemyDamageColor = Color.red;
    }
    
    // Thay đổi cài đặt popup
    public void SetPopupSettings(int newFontSize, float newHeightOffset, float newScale, float newDuration, float newDistance)
    {
        fontSize = newFontSize;
        popupHeightOffset = newHeightOffset;
        popupScale = newScale;
        popupDuration = newDuration;
        popupDistance = newDistance;
    }
}
