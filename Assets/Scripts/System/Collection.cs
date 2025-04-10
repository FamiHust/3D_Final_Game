using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public List<GameObject> cardObjects;
    public List<TextMeshProUGUI> cardTexts;

    public static int x;
    public int[] HowManyCards = new int[41];

    public bool openPack;
    public List<GameObject> cardObjects_2;
    public int[] o;
    public int oo;
    public int rand;
    public string card;

    void Start()
    {
        x = 1;
        for (int i = 1; i <= 40; i++)
        {
            HowManyCards[i] = PlayerPrefs.GetInt("x" + i, 0);
        }

        if (openPack == true)
        {
            for (int i = 0; i <= 4; i++)
            {
                getRandomCard();
            }
        }
    }

    void Update()
    {
        if (openPack == false)
        {
            for (int i = 0; i < 12; i++)
            {
                int cardID = x + i;
                if (cardID > 40) continue;

                cardObjects[i].GetComponent<CardInCollection>().thisID = cardID;
                cardTexts[i].text = "x" + HowManyCards[cardID];

                bool isZero = HowManyCards[cardID] == 0;
                cardObjects[i].GetComponent<CardInCollection>().beGrey = isZero;
            }
        }

        // Lưu lại PlayerPrefs
        for (int i = 1; i <= 40; i++)
        {
            PlayerPrefs.SetInt("x" + i, HowManyCards[i]);
        }

        if (openPack == true)
        {
            for (int i = 0; i <= 4; i++)
            {
                int cardID = i;
                // if (cardID > 40) continue;

                cardObjects_2[i].GetComponent<CardInCollection>().thisID = o[i];
            }
        }
    }

    public void UpButton()
    {
        x = Mathf.Max(1, x - 12);
    }

    public void DownButton()
    {
        x = Mathf.Min(40 - 11, x + 12);
    }

    public void ChangeCardAmount(int index, int delta)
    {
        int cardID = x + index;
        if (cardID < 1 || cardID > 40) return;

        HowManyCards[cardID] += delta;
        HowManyCards[cardID] = Mathf.Max(0, HowManyCards[cardID]);
    }

    public void Card1Plus() => ChangeCardAmount(0, 1);
    public void Card1Minus() => ChangeCardAmount(0, -1);

    public void Card2Plus() => ChangeCardAmount(1, 1);
    public void Card2Minus() => ChangeCardAmount(1, -1);

    public void Card3Plus() => ChangeCardAmount(2, 1);
    public void Card3Minus() => ChangeCardAmount(2, -1);

    public void Card4Plus() => ChangeCardAmount(3, 1);
    public void Card4Minus() => ChangeCardAmount(3, -1);

    public void Card5Plus() => ChangeCardAmount(4, 1);
    public void Card5Minus() => ChangeCardAmount(4, -1);

    public void Card6Plus() => ChangeCardAmount(5, 1);
    public void Card6Minus() => ChangeCardAmount(5, -1);

    public void Card7Plus() => ChangeCardAmount(6, 1);
    public void Card7Minus() => ChangeCardAmount(6, -1);

    public void Card8Plus() => ChangeCardAmount(7, 1);
    public void Card8Minus() => ChangeCardAmount(7, -1);

    public void Card9Plus() => ChangeCardAmount(8, 1);
    public void Card9Minus() => ChangeCardAmount(8, -1);

    public void Card10Plus() => ChangeCardAmount(9, 1);
    public void Card10Minus() => ChangeCardAmount(9, -1);

    public void Card11Plus() => ChangeCardAmount(10, 1);
    public void Card11Minus() => ChangeCardAmount(10, -1);

    public void Card12Plus() => ChangeCardAmount(11, 1);
    public void Card12Minus() => ChangeCardAmount(11, -1);

    public void getRandomCard()
    {
        rand = Random.Range(1, 29);
        PlayerPrefs.SetInt("x" + rand, (int)HowManyCards[rand]++);
        card = CardDatabase.cardList[rand].cardName;

        for (int i = 1; i <= 29; i++)
        {
            PlayerPrefs.SetInt("x" + i, (int)HowManyCards[i]);
        }
        o[oo] = rand;
        oo++;
    }
}
