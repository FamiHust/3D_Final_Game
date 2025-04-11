using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AICardToHand : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();

    public int thisID;
    public int id;
    public string cardName;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI descriptionText;

    public Sprite thisSprite;
    public Image thatImage;

    public static int DrawX;
    public int drawXcards;
    public int addXmaxMana;

    public int hurted;
    public int actualpower;
    public int returnXcards;

    public GameObject Hand;
    public int z = 0;
    public GameObject It;
    public int numberOfCardsInDeck;

    public bool isTarget;
    public GameObject Graveyard;
    public bool thisCardCanBeDestroyed;

    public GameObject cardBack;
    public GameObject[] AiZones = new GameObject[8];

    public bool canAttack;
    public bool summoningSickness;

    public bool isSummoned;
    public GameObject[] battleZones = new GameObject[8];

    public int healXpower;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDatabase.cardList[thisID];
        numberOfCardsInDeck = AI.deckSize;

        Hand = GameObject.Find("Enemy_Hand");
        z = 0;

        Graveyard = GameObject.Find("Enemy_Graveyard");
        StartCoroutine(AfterVoidStart());

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                AiZones[i] = GameObject.Find("Enemy_Zone");
            else
                AiZones[i] = GameObject.Find("Enemy_Zone" + i);
        }

        summoningSickness = true;

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                battleZones[i] = GameObject.Find("Enemy_Zone");
            else
                battleZones[i] = GameObject.Find("Enemy_Zone" + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (z == 0)
        {
            Hand = GameObject.Find("Enemy_Hand");
            It.transform.SetParent(Hand.transform);
            It.transform.localScale = Vector3.one;
            It.transform.localPosition = new Vector3(It.transform.localPosition.x, It.transform.localPosition.y, It.transform.localPosition.z);

            It.transform.eulerAngles = new Vector3(0, 0, 0);
            z = 1;
        }

        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        cost = thisCard[0].cost;
        attack = thisCard[0].attack;
        defense = thisCard[0].defense;
        cardDescription = thisCard[0].cardDescription;
        thisSprite = thisCard[0].thisImage;
        drawXcards = thisCard[0].drawXcards;
        addXmaxMana = thisCard[0].addXmaxMana;

        returnXcards = thisCard[0].returnXcards;

        nameText.text = "" + cardName;
        costText.text = "" + cost;

        actualpower = defense - hurted;

        atkText.text = "" + attack;
        defText.text = "" + actualpower;
        descriptionText.text = "" + cardDescription;
        thatImage.sprite = thisSprite;

        healXpower = thisCard[0].healXpower;

        if (this.tag == "Clones")
        {
            thisCard[0] = AI.staticEnemyDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            AI.deckSize -= 1;
            this.tag = "Untagged";
        }

        if (hurted >= defense && thisCardCanBeDestroyed == true)
        {
            this.transform.SetParent(Graveyard.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(45, 0, 0);
            hurted = 0;
        }

        if (this.transform.parent == Hand.transform)
        {
            cardBack.SetActive(true);
        }

        foreach (GameObject zone in AiZones)
        {
            if (this.transform.parent == zone.transform)
            {
                cardBack.SetActive(false);
                break;
            }
        }

        if (TurnSystem.isYourTurn == false && summoningSickness == false)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        foreach (GameObject zone in AiZones)
        {
            if (TurnSystem.isYourTurn == true && this.transform.parent == zone.transform)
            {
                summoningSickness = false;
                break;
            }
        }
        
        foreach (GameObject zone in battleZones)
        {
            if (this.transform.parent == zone.transform && isSummoned == false)
            {
                if (drawXcards > 0)
                {
                    DrawX = drawXcards;
                    isSummoned = true;
                    break;
                }

                if (id == 23)
                {
                    TurnSystem.maxEnemyMana += 2;
                    isSummoned = true;
                }

                if (healXpower > 0)
                {
                    EnemyHp.staticHp += healXpower;
                    isSummoned = true;
                }

                isSummoned = true;
            }
        }
    }

    public void BeingTarget()
    {
        isTarget = true;
    }

    public void DontBeingTarget()
    {
        isTarget = false;
    }

    IEnumerator AfterVoidStart()
    {
        yield return new WaitForSeconds(0.5f);
        thisCardCanBeDestroyed = true;
    }
}
