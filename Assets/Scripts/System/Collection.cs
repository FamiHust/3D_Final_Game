using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json; 

public class Collection : MonoBehaviour
{
    public List<GameObject> cardObjects;
    public List<TextMeshProUGUI> cardTexts;

    public static int x;
    public int[] HowManyCards = new int[136]; // Index từ 1 -> 117

    public bool openPack;
    public List<GameObject> cardObjects_2;
    public int[] o = new int[5]; 
    public int oo;
    public int rand;
    public string card;

    void Start()
    {
        x = 1;

        if (!openPack)
        {
            LoadCardsFromPlayfab(); // Load collection từ PlayFab
        }
        else
        {
            for (int i = 0; i <= 4; i++)
            {
                getRandomCard();
            }
        }
    }

    void Update()
    {
        if (!openPack)
        {
            for (int i = 0; i < 9; i++)
            {
                int cardID = x + i;
                if (cardID > 135) continue;

                cardObjects[i].GetComponent<CardInCollection>().thisID = cardID;
                cardTexts[i].text = "x" + HowManyCards[cardID];

                bool isZero = HowManyCards[cardID] == 0;
                cardObjects[i].GetComponent<CardInCollection>().beGrey = isZero;
            }
        }
        else
        {
            for (int i = 0; i <= 4; i++)
            {
                cardObjects_2[i].GetComponent<CardInCollection>().thisID = o[i];
            }
        }
    }

    public void UpButton()
    {
        if (x - 9 >= 1) x -= 9;
    }

    public void DownButton()
    {
        if (x + 9 <= 135) x += 9;
    }

    public void ChangeCardAmount(int index, int delta)
    {
        int cardID = x + index;
        if (cardID < 1 || cardID > 135) return;

        HowManyCards[cardID] += delta;
        HowManyCards[cardID] = Mathf.Max(0, HowManyCards[cardID]);

        SaveCardsToPlayfab(); // Lưu sau khi thay đổi
    }

    // Các hàm + và - từng card
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

    public void getRandomCard()
    {
        rand = Random.Range(1, 135);
        HowManyCards[rand]++;
        card = CardDatabase.cardList[rand].cardName;

        o[oo] = rand;
        oo++;

        SaveCardsToPlayfab(); // Lưu sau khi mở pack
    }

    public void SaveCardsToPlayfab()
    {
        string json = JsonConvert.SerializeObject(HowManyCards);

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "CardCollection", json }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Card data saved to PlayFab.");
        }, error =>
        {
            Debug.LogError("Save failed: " + error.GenerateErrorReport());
        });
    }

    public void LoadCardsFromPlayfab(System.Action onDone = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("CardCollection"))
            {
                string json = result.Data["CardCollection"].Value;
                HowManyCards = JsonConvert.DeserializeObject<int[]>(json);
                Debug.Log("Card data loaded from PlayFab.");
            }
            else
            {
                Debug.Log("No existing card data, initializing...");
                HowManyCards = new int[136];

                // Gán mặc định 40 lá đầu tiên, mỗi lá 1 cái
                for (int i = 1; i <= 60; i++)
                {
                    HowManyCards[i] = 1;
                }

                // Lưu dữ liệu khởi tạo lên PlayFab
                SaveCardsToPlayfab();
            }

            onDone?.Invoke();
        }, error =>
        {
            Debug.LogError("Load failed: " + error.GenerateErrorReport());
        });
    }

}
