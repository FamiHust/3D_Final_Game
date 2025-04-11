using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoDisplay : MonoBehaviour
{
    public Image cardImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI costText;

    public static CardInfoDisplay instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false); // Ẩn UI lúc đầu
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
