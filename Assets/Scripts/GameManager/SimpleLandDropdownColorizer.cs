using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SimpleLandDropdownColorizer : MonoBehaviour
{
    [Header("Element Colors")]
    [SerializeField] private Color earthColor = new Color(0.7f, 0.9f, 0.7f, 1f);      // Xanh nhạt
    [SerializeField] private Color waterColor = new Color(0.4f, 0.6f, 1f, 1f);       // Xanh dương  
    [SerializeField] private Color forestColor = new Color(0.4f, 0.8f, 0.6f, 1f);    // Xanh ngọc
    [SerializeField] private Color swampColor = new Color(0.2f, 0.6f, 0.3f, 1f);     // Xanh lá đậm
    
    [Header("Default Colors")]
    [SerializeField] private Color defaultDropdownColor = Color.white;                 // Màu trắng ban đầu
    [SerializeField] private Color defaultTextColor = Color.white;                     // Màu text ban đầu (trắng)
    
    [Header("Dropdown References")]
    [SerializeField] private TMP_Dropdown[] landDropdowns;
    
    private void Start()
    {
        if (landDropdowns == null || landDropdowns.Length == 0)
        {
            // Tự động tìm dropdowns nếu không được gán
            landDropdowns = FindObjectsOfType<TMP_Dropdown>();
        }
        
        // Thiết lập màu sắc ban đầu cho từng dropdown (màu trắng)
        SetupInitialColors();
        
        // Thiết lập màu sắc cho các option trong dropdown
        SetupDropdownOptions();
    }
    
    private void SetupInitialColors()
    {
        // Tất cả dropdown ban đầu có màu trắng
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            if (landDropdowns[i] != null)
            {
                ApplyDefaultColor(landDropdowns[i]);
            }
        }
    }
    
    private void SetupDropdownOptions()
    {
        // Thiết lập màu sắc cho các option trong dropdown
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            if (landDropdowns[i] != null)
            {
                SetupDropdownOptionColors(landDropdowns[i]);
            }
        }
    }
    
    private void ApplyDefaultColor(TMP_Dropdown dropdown)
    {
        if (dropdown == null) return;
        
        Debug.Log($"Applying default color to dropdown: {dropdown.name}");
        
        // Thay đổi màu background của dropdown thành màu trắng
        var backgroundImage = dropdown.GetComponent<Image>();
        if (backgroundImage != null)
        {
            backgroundImage.color = defaultDropdownColor;
            Debug.Log($"Set background color to: {defaultDropdownColor}");
        }
        
        // Thay đổi màu text của dropdown thành màu trắng
        var label = dropdown.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null)
        {
            Debug.Log($"Found label: {label.name}, current color: {label.color}");
            label.color = defaultTextColor;
            Debug.Log($"Set label color to: {defaultTextColor}");
        }
        else
        {
            Debug.LogWarning("No TextMeshProUGUI found in dropdown!");
        }
        
        // Thay đổi màu arrow của dropdown thành màu trắng
        var arrow = dropdown.transform.Find("Arrow");
        if (arrow != null)
        {
            var arrowImage = arrow.GetComponent<Image>();
            if (arrowImage != null)
            {
                arrowImage.color = defaultTextColor;
                Debug.Log($"Set arrow color to: {defaultTextColor}");
            }
        }
        
        // Force update để đảm bảo thay đổi được áp dụng
        // dropdown.SetAllDirty(); // TMP_Dropdown không có method này
        Canvas.ForceUpdateCanvases();
        ForceUpdateUI();
    }
    
    private void SetupDropdownOptionColors(TMP_Dropdown dropdown)
    {
        if (dropdown == null) return;
        
        // Thiết lập màu sắc cho các option trong dropdown
        var template = dropdown.transform.Find("Template");
        if (template != null)
        {
            var viewport = template.Find("Viewport");
            if (viewport != null)
            {
                var content = viewport.Find("Content");
                if (content != null)
                {
                    // Tìm tất cả các item trong dropdown
                    for (int i = 0; i < content.childCount; i++)
                    {
                        var item = content.GetChild(i);
                        if (item != null)
                        {
                            // Áp dụng màu theo element type tương ứng với index
                            ElementType elementType = (ElementType)i;
                            Color elementColor = GetElementColor(elementType);
                            // Tất cả text đều có màu trắng thay vì tự động tính toán
                            Color textColor = Color.white;
                            
                            // Thay đổi màu background của item
                            var itemImage = item.GetComponent<Image>();
                            if (itemImage != null)
                            {
                                itemImage.color = elementColor;
                            }
                            
                            // Thay đổi màu text của item thành màu trắng
                            var itemText = item.GetComponentInChildren<TextMeshProUGUI>();
                            if (itemText != null)
                            {
                                itemText.color = textColor;
                            }
                        }

                    }
                }
            }
        }
    }
    
    private void ApplyElementColor(TMP_Dropdown dropdown, ElementType elementType)
    {
        if (dropdown == null) return;
        
        Color elementColor = GetElementColor(elementType);
        // Text luôn có màu trắng để dễ đọc
        Color textColor = Color.white;
        
        // Thay đổi màu background của dropdown theo element type đã chọn
        var backgroundImage = dropdown.GetComponent<Image>();
        if (backgroundImage != null)
        {
            backgroundImage.color = elementColor;
        }
        
        // Thay đổi màu text của dropdown thành màu trắng
        var label = dropdown.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null)
        {
            label.color = textColor;
        }
        
        // Thay đổi màu arrow của dropdown thành màu trắng
        var arrow = dropdown.transform.Find("Arrow");
        if (arrow != null)
        {
            var arrowImage = arrow.GetComponent<Image>();
            if (arrowImage != null)
            {
                arrowImage.color = textColor;
            }
        }
        
        // KHÔNG thay đổi màu template background để giữ nguyên màu options
        // var template = dropdown.transform.Find("Template");
        // if (template != null)
        // {
        //     var templateImage = template.GetComponent<Image>();
        //     if (templateImage != null)
        //     {
        //         templateImage.color = elementColor;
        //     }
        // }
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
    
    // Public method để thay đổi màu sắc của dropdown cụ thể
    public void UpdateDropdownColor(int dropdownIndex, ElementType elementType)
    {
        if (dropdownIndex >= 0 && dropdownIndex < landDropdowns.Length)
        {
            ApplyElementColor(landDropdowns[dropdownIndex], elementType);
        }
    }
    
    // Public method để thay đổi màu sắc của dropdown theo dropdown value
    public void UpdateDropdownColorByValue(TMP_Dropdown dropdown, int dropdownValue)
    {
        if (dropdown == null) return;
        
        ElementType elementType = (ElementType)dropdownValue;
        ApplyElementColor(dropdown, elementType);
    }
    
    // Public method để reset dropdown về màu trắng
    public void ResetDropdownToDefault(int dropdownIndex)
    {
        if (dropdownIndex >= 0 && dropdownIndex < landDropdowns.Length)
        {
            ApplyDefaultColor(landDropdowns[dropdownIndex]);
        }
    }
    
    // Public method để refresh tất cả màu sắc về trạng thái ban đầu
    public void RefreshAllColors()
    {
        SetupInitialColors();
    }
    
    // Public method để refresh tất cả dropdown về màu trắng
    public void ResetAllDropdownsToDefault()
    {
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            if (landDropdowns[i] != null)
            {
                ApplyDefaultColor(landDropdowns[i]);
            }
        }
    }
    
    // Method test để debug màu sắc
    [ContextMenu("Test Default Colors")]
    public void TestDefaultColors()
    {
        Debug.Log($"Default Dropdown Color: {defaultDropdownColor}");
        Debug.Log($"Default Text Color: {defaultTextColor}");
        
        if (landDropdowns != null && landDropdowns.Length > 0)
        {
            var firstDropdown = landDropdowns[0];
            if (firstDropdown != null)
            {
                var label = firstDropdown.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null)
                {
                    Debug.Log($"First dropdown text color: {label.color}");
                    // Force apply default color
                    label.color = defaultTextColor;
                    Debug.Log($"After applying default color: {label.color}");
                }
            }
        }
    }
    
    // Method để force override tất cả text colors
    [ContextMenu("Force Override All Text Colors")]
    public void ForceOverrideAllTextColors()
    {
        Debug.Log("Force overriding all text colors to white...");
        
        if (landDropdowns == null || landDropdowns.Length == 0)
        {
            Debug.LogWarning("No dropdowns found!");
            return;
        }
        
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            if (landDropdowns[i] != null)
            {
                ForceOverrideDropdownTextColors(landDropdowns[i]);
            }
        }
        
        Debug.Log("Force override completed!");
    }
    
    private void ForceOverrideDropdownTextColors(TMP_Dropdown dropdown)
    {
        if (dropdown == null) return;
        
        Debug.Log($"Force overriding text colors for dropdown: {dropdown.name}");
        
        // Tìm tất cả TextMeshProUGUI components trong dropdown
        var allTexts = dropdown.GetComponentsInChildren<TextMeshProUGUI>(true);
        Debug.Log($"Found {allTexts.Length} text components");
        
        foreach (var text in allTexts)
        {
            if (text != null)
            {
                Debug.Log($"Overriding text color: {text.name} from {text.color} to {defaultTextColor}");
                text.color = defaultTextColor;
            }
        }
        
        // Force update
        // dropdown.SetAllDirty(); // TMP_Dropdown không có method này
        Canvas.ForceUpdateCanvases();
        ForceUpdateUI();
    }
    
    private void ForceUpdateUI()
    {
        // Force update tất cả UI elements
        Canvas.ForceUpdateCanvases();
        
        // Không cần LayoutRebuilder vì nó không phải MonoBehaviour
        // Chỉ cần ForceUpdateCanvases là đủ
    }
}

