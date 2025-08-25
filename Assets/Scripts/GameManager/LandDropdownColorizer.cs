using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LandDropdownColorizer : MonoBehaviour
{
    [Header("Element Colors")]
    [SerializeField] private Color earthColor = new Color(0.7f, 0.9f, 0.7f, 1f);      // Xanh nhạt
    [SerializeField] private Color waterColor = new Color(0.4f, 0.6f, 1f, 1f);       // Xanh dương
    [SerializeField] private Color forestColor = new Color(0.4f, 0.8f, 0.6f, 1f);    // Xanh ngọc
    [SerializeField] private Color swampColor = new Color(0.2f, 0.6f, 0.3f, 1f);     // Xanh lá đậm
    
    [Header("Dropdown References")]
    [SerializeField] private TMP_Dropdown[] landDropdowns;
    
    [Header("Color Settings")]
    [SerializeField] private bool colorizeDropdownBackground = true;
    [SerializeField] private bool colorizeDropdownText = true;
    [SerializeField] private bool colorizeDropdownArrow = true;
    
    private void Start()
    {
        if (landDropdowns == null || landDropdowns.Length == 0)
        {
            // Tự động tìm dropdowns nếu không được gán
            landDropdowns = FindObjectsOfType<TMP_Dropdown>();
        }
        
        // Thiết lập màu sắc cho từng dropdown
        SetupDropdownColors();
    }
    
    private void SetupDropdownColors()
    {
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            if (landDropdowns[i] != null)
            {
                ColorizeDropdown(landDropdowns[i], (ElementType)i);
            }
        }
    }
    
    private void ColorizeDropdown(TMP_Dropdown dropdown, ElementType elementType)
    {
        if (dropdown == null) return;
        
        Color elementColor = GetElementColor(elementType);
        
        // Thay đổi màu background của dropdown
        if (colorizeDropdownBackground)
        {
            var backgroundImage = dropdown.GetComponent<Image>();
            if (backgroundImage != null)
            {
                backgroundImage.color = elementColor;
            }
        }
        
        // Thay đổi màu text của dropdown
        if (colorizeDropdownText)
        {
            var label = dropdown.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.color = GetContrastColor(elementColor);
            }
        }
        
        // Thay đổi màu arrow của dropdown
        if (colorizeDropdownArrow)
        {
            var arrow = dropdown.transform.Find("Arrow");
            if (arrow != null)
            {
                var arrowImage = arrow.GetComponent<Image>();
                if (arrowImage != null)
                {
                    arrowImage.color = GetContrastColor(elementColor);
                }
            }
        }
        
        // Thay đổi màu template background (khi mở dropdown)
        var template = dropdown.transform.Find("Template");
        if (template != null)
        {
            var templateImage = template.GetComponent<Image>();
            if (templateImage != null)
            {
                templateImage.color = elementColor;
            }
            
            // Thay đổi màu của các item trong dropdown
            var viewport = template.Find("Viewport");
            if (viewport != null)
            {
                var content = viewport.Find("Content");
                if (content != null)
                {
                    for (int i = 0; i < content.childCount; i++)
                    {
                        var item = content.GetChild(i);
                        var itemImage = item.GetComponent<Image>();
                        if (itemImage != null)
                        {
                            itemImage.color = elementColor;
                        }
                        
                        // Thay đổi màu text của item
                        var itemText = item.GetComponentInChildren<TextMeshProUGUI>();
                        if (itemText != null)
                        {
                            itemText.color = GetContrastColor(elementColor);
                        }
                    }
                }
            }
        }
    }
    
    private Color GetElementColor(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Earth:
                return earthColor;
            case ElementType.Water:
                return waterColor;
            case ElementType.Forest:
                return forestColor;
            case ElementType.Swamp:
                return swampColor;
            default:
                return Color.white;
        }
    }
    
    private Color GetContrastColor(Color backgroundColor)
    {
        // Tính độ sáng của background
        float luminance = 0.299f * backgroundColor.r + 0.587f * backgroundColor.g + 0.114f * backgroundColor.b;
        
        // Trả về màu đen hoặc trắng tùy theo độ sáng của background
        return luminance > 0.5f ? Color.black : Color.white;
    }
    
    // Public method để thay đổi màu sắc động
    public void UpdateDropdownColor(int dropdownIndex, ElementType elementType)
    {
        if (dropdownIndex >= 0 && dropdownIndex < landDropdowns.Length)
        {
            ColorizeDropdown(landDropdowns[dropdownIndex], elementType);
        }
    }
    
    // Public method để thay đổi màu sắc của tất cả dropdown
    public void RefreshAllDropdownColors()
    {
        SetupDropdownColors();
    }
}

