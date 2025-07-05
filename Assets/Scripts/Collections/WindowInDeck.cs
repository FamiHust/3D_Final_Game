using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class WindowInDeck : MonoBehaviour, IPointerClickHandler
{
    public GameObject Panel;
    public Text nameText;

    public int id;
    public int quantityOf;
    public GameObject Creator;

    void Start()
    {
        Panel = GameObject.Find("Panel");
        Creator = GameObject.Find("Collection_UI_Panel");
        transform.SetParent(Panel.transform);
        transform.localScale = Vector3.one;

        id = DeckCreator.lastAdded;
    }

    void Update()
    {
        quantityOf = Creator.GetComponent<DeckCreator>().quantity[id];
        nameText.text = CardDatabase.cardList[id].cardName + " x" + quantityOf;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Gọi hàm xóa bài trong Deck
        Creator.GetComponent<DeckCreator>().RemoveCardFromDeck(id);
    }
}
