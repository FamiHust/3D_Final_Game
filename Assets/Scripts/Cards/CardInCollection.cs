using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInCollection : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisID;
    public int id;
    public string cardName;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Sprite thisSprite;
    [SerializeField] private Image thatImage;
    [SerializeField] private Color originalColor;

    public bool beGrey;
    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDatabase.cardList[thisID];

        originalColor = thatImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        thisCard[0] = CardDatabase.cardList[thisID];

        Card card = thisCard[0];
        id = card.id;
        cardName = card.cardName;
        cost = card.cost;
        attack = card.attack;
        defense = card.defense;
        cardDescription = card.cardDescription;
        thisSprite = card.thisImage;

        nameText.text = cardName;
        costText.text = cost.ToString();
        atkText.text = attack.ToString();
        defText.text = defense.ToString();
        descriptionText.text = cardDescription;
        thatImage.sprite = thisSprite;

        if (beGrey == true)
        {
            thatImage.color = new Color32(115, 115, 115, 255);
        }
        else
        {
            thatImage.color = originalColor;
        }
    }

    public void OnCardClick()
    {
        CardInfoDisplay.instance.ShowCardInfo(thisCard[0]);
    }

    public void OnCardExit()
    {
        CardInfoDisplay.instance.HideCardInfo();
    }
}
