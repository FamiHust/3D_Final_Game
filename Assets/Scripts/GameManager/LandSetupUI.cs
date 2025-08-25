using UnityEngine;
using TMPro;

public class LandSetupUI : MonoBehaviour
{
    public LandGroup[] lands;              
    public TMP_Dropdown[] landDropdowns;
    public GameObject Buttons;
    
    [Header("Dropdown Colorizer")]
    [SerializeField] private SimpleLandDropdownColorizer dropdownColorizer;
    
    void Awake()
    {
        // Tự động tìm dropdown colorizer nếu không được gán
        if (dropdownColorizer == null)
        {
            dropdownColorizer = FindObjectOfType<SimpleLandDropdownColorizer>();
        }
        
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            int index = i;
            landDropdowns[i].onValueChanged.AddListener((value) => OnDropdownChanged(index, value));
        }
    }
    
    void Start()
    {
        // Thiết lập màu sắc ban đầu cho các dropdown
        if (dropdownColorizer != null)
        {
            dropdownColorizer.RefreshAllColors();
        }
    }

    void OnDropdownChanged(int landIndex, int dropdownValue)
    {
        ElementType chosenElement = (ElementType)dropdownValue;
        lands[landIndex].SetLandElement(chosenElement);
        
        // Cập nhật màu sắc của dropdown theo element type mới
        // Sử dụng UpdateDropdownColorByValue để tránh lỗi index
        if (dropdownColorizer != null)
        {
            dropdownColorizer.UpdateDropdownColorByValue(landDropdowns[landIndex], dropdownValue);
        }
    }

    public void ConfirmElementSetup()
    {
        gameObject.SetActive(false);
        Buttons.SetActive(true);
        TurnSystem.landConfirmed = true;

        FindObjectOfType<TurnSystem>().StartGame();
        FindObjectOfType<PlayerDeck>().StartGame();
        FindObjectOfType<AI>().AIStartGame();
    }
}
