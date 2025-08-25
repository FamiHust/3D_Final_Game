using UnityEngine;
using TMPro;

public class LandDropdownColorDemo : MonoBehaviour
{
    [Header("Demo Controls")]
    [SerializeField] private bool enableDemoMode = true;
    [SerializeField] private KeyCode refreshKey = KeyCode.R;
    [SerializeField] private KeyCode cycleColorsKey = KeyCode.C;
    [SerializeField] private KeyCode resetToDefaultKey = KeyCode.D;
    
    [Header("References")]
    [SerializeField] private SimpleLandDropdownColorizer colorizer;
    
    private int currentColorIndex = 0;
    private ElementType[] allElements = { ElementType.Earth, ElementType.Water, ElementType.Forest, ElementType.Swamp };
    
    void Start()
    {
        if (colorizer == null)
        {
            colorizer = FindObjectOfType<SimpleLandDropdownColorizer>();
        }
        
        if (enableDemoMode)
        {
            Debug.Log("Land Dropdown Color Demo Started!");
            Debug.Log("Press 'R' to refresh all colors");
            Debug.Log("Press 'C' to cycle through element colors");
            Debug.Log("Press 'D' to reset all dropdowns to default (white)");
        }
    }
    
    void Update()
    {
        if (!enableDemoMode || colorizer == null) return;
        
        // Refresh tất cả màu sắc
        if (Input.GetKeyDown(refreshKey))
        {
            colorizer.RefreshAllColors();
            Debug.Log("Refreshed all dropdown colors to default!");
        }
        
        // Cycle qua các element colors
        if (Input.GetKeyDown(cycleColorsKey))
        {
            currentColorIndex = (currentColorIndex + 1) % allElements.Length;
            ElementType newElement = allElements[currentColorIndex];
            
            // Áp dụng màu mới cho tất cả dropdown
            for (int i = 0; i < 4; i++)
            {
                colorizer.UpdateDropdownColor(i, newElement);
            }
            
            Debug.Log($"Cycled to {newElement} color for all dropdowns!");
        }
        
        // Reset tất cả dropdown về màu trắng
        if (Input.GetKeyDown(resetToDefaultKey))
        {
            colorizer.ResetAllDropdownsToDefault();
            Debug.Log("Reset all dropdowns to default white color!");
        }
    }
    
    // Public method để test từ Inspector
    [ContextMenu("Test Refresh Colors")]
    public void TestRefreshColors()
    {
        if (colorizer != null)
        {
            colorizer.RefreshAllColors();
            Debug.Log("Test: Refreshed all dropdown colors to default!");
        }
    }
    
    [ContextMenu("Test Reset All To Default")]
    public void TestResetAllToDefault()
    {
        if (colorizer != null)
        {
            colorizer.ResetAllDropdownsToDefault();
            Debug.Log("Test: Reset all dropdowns to default white color!");
        }
    }
    
    [ContextMenu("Test UpdateDropdownColorByValue")]
    public void TestUpdateDropdownColorByValue()
    {
        if (colorizer != null)
        {
            // Tìm dropdown đầu tiên để test
            var dropdowns = FindObjectsOfType<TMP_Dropdown>();
            if (dropdowns.Length > 0)
            {
                // Test với từng element type
                for (int i = 0; i < 4; i++)
                {
                    colorizer.UpdateDropdownColorByValue(dropdowns[0], i);
                    Debug.Log($"Test: Applied element type {(ElementType)i} to first dropdown!");
                }
            }
        }
    }
    
    [ContextMenu("Test Default Colors")]
    public void TestDefaultColors()
    {
        if (colorizer != null)
        {
            colorizer.TestDefaultColors();
        }
    }
    
    [ContextMenu("Test Force Override All Text Colors")]
    public void TestForceOverrideAllTextColors()
    {
        if (colorizer != null)
        {
            colorizer.ForceOverrideAllTextColors();
        }
    }
    
    [ContextMenu("Test Earth Colors")]
    public void TestEarthColors()
    {
        if (colorizer != null)
        {
            for (int i = 0; i < 4; i++)
            {
                colorizer.UpdateDropdownColor(i, ElementType.Earth);
            }
            Debug.Log("Test: Applied Earth colors to all dropdowns!");
        }
    }
    
    [ContextMenu("Test Water Colors")]
    public void TestWaterColors()
    {
        if (colorizer != null)
        {
            for (int i = 0; i < 4; i++)
            {
                colorizer.UpdateDropdownColor(i, ElementType.Water);
            }
            Debug.Log("Test: Applied Water colors to all dropdowns!");
        }
    }
    
    [ContextMenu("Test Forest Colors")]
    public void TestForestColors()
    {
        if (colorizer != null)
        {
            for (int i = 0; i < 4; i++)
            {
                colorizer.UpdateDropdownColor(i, ElementType.Forest);
            }
            Debug.Log("Test: Applied Forest colors to all dropdowns!");
        }
    }
    
    [ContextMenu("Test Swamp Colors")]
    public void TestSwampColors()
    {
        if (colorizer != null)
        {
            for (int i = 0; i < 4; i++)
            {
                colorizer.UpdateDropdownColor(i, ElementType.Swamp);
            }
            Debug.Log("Test: Applied Swamp colors to all dropdowns!");
        }
    }
    
    [ContextMenu("Test Individual Dropdown Colors")]
    public void TestIndividualDropdownColors()
    {
        if (colorizer != null)
        {
            // Test từng dropdown với màu khác nhau
            colorizer.UpdateDropdownColor(0, ElementType.Earth);
            colorizer.UpdateDropdownColor(1, ElementType.Water);
            colorizer.UpdateDropdownColor(2, ElementType.Forest);
            colorizer.UpdateDropdownColor(3, ElementType.Swamp);
            Debug.Log("Test: Applied different colors to each dropdown!");
        }
    }
}
