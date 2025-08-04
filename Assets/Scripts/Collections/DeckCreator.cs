using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class DeckCreator : MonoBehaviour
{
    public static int[] lastDeckLoaded; // <-- Biến tĩnh dùng truyền deck sang scene khác

    public int[] cardsWithThisID; // Mảng lưu số lượng từng lá trong deck
    public bool mouseOverDeck;
    public int dragged;
    public int numberOfCardsInDatabase;
    public int sum;
    public int numberOfDifferentCards;

    public GameObject coll;
    public GameObject prefab;

    public int[] saveDeck;
    public bool[] alreadyCreated;
    public static int lastAdded;
    public int[] quantity;
    public int maxCards = 40;
    public Text cardCountText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sum = 0;
        cardCountText.text = $"Bộ bài: {sum}/{maxCards}";

        // Có thể load deck ở đây nếu cần (hoặc load thủ công bên ngoài)
        // LoadDeckFromPlayfab();
    }

    public void EnterDeck()
    {
        mouseOverDeck = true;
    }

    public void ExitDeck()
    {
        mouseOverDeck = false;
    }

    public void Card1() { dragged = Collection.x; }
    public void Card2() { dragged = Collection.x+1; }
    public void Card3() { dragged = Collection.x+2; }
    public void Card4() { dragged = Collection.x+3; }
    public void Card5() { dragged = Collection.x+4; }
    public void Card6() { dragged = Collection.x+5; }
    public void Card7() { dragged = Collection.x+6; }
    public void Card8() { dragged = Collection.x+7; }
    public void Card9() { dragged = Collection.x+8; }

    public void Drop()
    {
        if (mouseOverDeck && coll.GetComponent<Collection>().HowManyCards[dragged] > 0)
        {
            int currentTotal = 0;
            for (int i = 0; i < numberOfCardsInDatabase; i++)
            {
                currentTotal += cardsWithThisID[i];
            }

            if (currentTotal >= maxCards)
            {
                Debug.Log("Đã đủ 40 lá, không thể thêm nữa.");
                return;
            }

            cardsWithThisID[dragged]++;
            if (cardsWithThisID[dragged] < 0) cardsWithThisID[dragged] = 0;

            coll.GetComponent<Collection>().HowManyCards[dragged]--;

            CalculateDrop();
            UpdateCardCountDisplay();

            SoundManager.PlaySound(SoundType.Drop);
        }
    }

    public void CalculateDrop()
    {
        lastAdded = 0;
        int i = dragged;

        if (cardsWithThisID[i] > 0 && alreadyCreated[i] == false)
        {
            lastAdded = i;
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
            alreadyCreated[i] = true;

            quantity[i] = 1;
        }
        else if (cardsWithThisID[i] > 0 && alreadyCreated[i] == true)
        {
            quantity[i]++;
        }
    }

    public void SaveDeckToPlayfab()
    {
        string json = JsonConvert.SerializeObject(cardsWithThisID);
        
        // Save to PlayerPrefs
        PlayerPrefs.SetString("DeckData", json);
        PlayerPrefs.Save();

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Deck", json }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Deck saved to PlayFab.");
            lastDeckLoaded = (int[])cardsWithThisID.Clone();
        }, error =>
        {
            Debug.LogError("Save deck failed: " + error.GenerateErrorReport());
        });
    }

    // LOAD deck từ PlayFab
    public void LoadDeckFromPlayfab(System.Action onDone = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("Deck"))
            {
                string json = result.Data["Deck"].Value;
                cardsWithThisID = JsonConvert.DeserializeObject<int[]>(json);
                Debug.Log("Deck loaded from PlayFab.");
            }
            else
            {
                Debug.Log("No deck found, initializing default deck...");
                cardsWithThisID = new int[numberOfCardsInDatabase];
                for (int i = 0; i < 40; i++) cardsWithThisID[i] = 1; // 40 lá đầu tiên mỗi lá 1
                SaveDeckToPlayfab();
            }
            // Luôn gán lại cho biến static sau khi load (hoặc tạo mới)
            lastDeckLoaded = (int[])cardsWithThisID.Clone();
            onDone?.Invoke();
        }, error =>
        {
            Debug.LogError("Load deck failed: " + error.GenerateErrorReport());
            onDone?.Invoke();
        });
    }

    // public void CreateDeck()
    // {
    //     sum = 0;
    //     for (int i = 0; i < numberOfCardsInDatabase; i++)
    //     {
    //         sum += cardsWithThisID[i];
    //     }

    //     if (sum == 40)
    //     {
    //         SaveDeckToPlayfab();
    //         Debug.Log("Deck saved to PlayFab!");
    //     }
    //     else
    //     {
    //         Debug.Log("Deck must have exactly 40 cards.");
    //     }

    //     sum = 0;
    //     numberOfDifferentCards = 0;

    //     for (int i = 0; i < numberOfCardsInDatabase; i++)
    //     {
    //         saveDeck[i] = cardsWithThisID[i];
    //     }
    // }
    public void CreateDeck()
    {
        // Tính tổng số lá
        sum = 0;
        numberOfDifferentCards = 0;

        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            sum += cardsWithThisID[i];
            if (cardsWithThisID[i] > 0)
                numberOfDifferentCards++;
        }

        if (sum == maxCards)
        {
            SaveDeckToPlayfab();

            // Lưu vào saveDeck
            for (int i = 0; i < numberOfCardsInDatabase; i++)
            {
                saveDeck[i] = cardsWithThisID[i];
            }

            Debug.Log("Deck saved to PlayFab!");
        }
        else
        {
            Debug.LogWarning($"Deck must have exactly {maxCards} cards. Currently: {sum}");
        }
    }


    public void UpdateCardCountDisplay()
    {
        sum = 0;
        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            sum += cardsWithThisID[i];
        }

        cardCountText.text = $"Bộ bài: {sum}/{maxCards}";
    }

    public void RemoveCardFromDeck(int id)
    {
        if (cardsWithThisID[id] > 0)
        {
            cardsWithThisID[id]--;
            quantity[id]--;

            coll.GetComponent<Collection>().HowManyCards[id]++;

            UpdateCardCountDisplay();

            if (quantity[id] <= 0)
            {
                alreadyCreated[id] = false;

                WindowInDeck[] allCards = FindObjectsOfType<WindowInDeck>();
                foreach (var card in allCards)
                {
                    if (card.id == id)
                    {
                        SoundManager.PlaySound(SoundType.Drop);
                        Destroy(card.gameObject);
                        break;
                    }
                }
            }
        }
    }
}
