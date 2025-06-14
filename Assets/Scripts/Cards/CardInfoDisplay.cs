using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoDisplay : MonoBehaviour
{
    public static CardInfoDisplay instance;
    
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI costText;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false); 
    }

    public void ShowCardInfo(Card card)
    {
        cardImage.sprite = card.thisImage;
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
        atkText.text = "ATK: " + card.attack;
        defText.text = "DEF: " + card.defense;
        costText.text = "Cost: " + card.cost;
        gameObject.SetActive(true);
    }

    public void HideCardInfo()
    {
        gameObject.SetActive(false);
    }
}
