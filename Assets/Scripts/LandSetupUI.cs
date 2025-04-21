using UnityEngine;
using TMPro;

public class LandSetupUI : MonoBehaviour
{
    public LandGroup[] lands;              
    public TMP_Dropdown[] landDropdowns;
    public GameObject Buttons;  

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
        gameObject.SetActive(false);
        Buttons.SetActive(true);
        TurnSystem.landConfirmed = true;

        FindObjectOfType<PlayerDeck>().StartGame(); 
        FindObjectOfType<AI>().AIStartGame();
    }
}
