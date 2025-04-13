using UnityEngine;
using TMPro;

public class LandSetupUI : MonoBehaviour
{
    public LandGroup[] lands;              
    public TMP_Dropdown[] landDropdowns;   

    void Awake()
    {
        for (int i = 0; i < landDropdowns.Length; i++)
        {
            int index = i;
            landDropdowns[i].onValueChanged.AddListener((value) => OnDropdownChanged(index, value));
        }
    }

    void OnDropdownChanged(int landIndex, int dropdownValue)
    {
        ElementType chosenElement = (ElementType)dropdownValue;
        lands[landIndex].SetLandElement(chosenElement);
    }

    public void ConfirmElementSetup()
    {
        // giữ lại nếu sau này m muốn làm gì khi nhấn Confirm
        gameObject.SetActive(false);
        TurnSystem.landConfirmed = true;
    }
}
