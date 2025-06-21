// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerDeck : MonoBehaviour
// {
//     public List<Card> deck = new List<Card>();
//     public List<Card> container = new List<Card>();
//     public static List<Card> staticDeck = new List<Card>();

//     public int x;
//     public static int deckSize;
//     public TextMeshProUGUI deckSizeText;

//     public GameObject cardIndex1;
//     public GameObject cardIndex2;
//     public GameObject cardIndex3;
//     public GameObject cardIndex4;
//     public GameObject cardIndex5;
//     public GameObject cardIndex6;
//     public GameObject cardIndex7;
//     public GameObject CardToHand;
//     public GameObject CardBack;
//     public GameObject Deck;
//     public GameObject[] Clones;
//     public GameObject Hand;

//     public TextMeshProUGUI loseText;
//     public GameObject LoseTextGameObject;

//     void Awake()
//     {
//         Shuffle();
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         x = 0;
//         deckSize = 40;

//         for (int i = 1; i <= 135; i++)
//         {
//             if (PlayerPrefs.GetInt("deck" + i, 0) > 0)
//             {
//                 for (int j = 1; j <= PlayerPrefs.GetInt("deck" + i, 0); j++)
//                 {
//                     deck[x] = CardDatabase.cardList[i];
//                     x++;
//                 }
//             }
//         }
//         Shuffle();

//         // StartGame();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (deckSize <= 0)
//         {
//             LoseTextGameObject.SetActive(true);
//             loseText.text = "YOU LOSE";
//         }
//         int handSize = Hand.transform.childCount;
        
//         staticDeck = deck;
        
//         if (deckSize < 30) cardIndex1.SetActive(false);
//         if (deckSize < 20) cardIndex2.SetActive(false);
//         if (deckSize < 10) cardIndex3.SetActive(false);
//         if (deckSize < 5)  cardIndex4.SetActive(false);
//         if (deckSize < 3)  cardIndex5.SetActive(false);
//         if (deckSize < 2)  cardIndex6.SetActive(false);
//         if (deckSize < 1)  cardIndex7.SetActive(false);


//         if (ThisCard.drawX > 0)
//         {
//             StartCoroutine(Draw(ThisCard.drawX));
//             ThisCard.drawX = 0;
//         }

//         if (TurnSystem.startTurn == true && TurnSystem.isYourTurn)
//         {
//             if (handSize < 5)
//             {
//                 StartCoroutine(Draw(1));
//             }
//             TurnSystem.startTurn = false;
//         }

//         deckSizeText.text = "Deck: " + deckSize;
//     }

//     public void StartGame()
//     {
//         StartCoroutine(StartGameCoroutine());
//     }

//     IEnumerator StartGameCoroutine()
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             yield return new WaitForSeconds(1f);
//             Instantiate(CardToHand, transform.position, transform.rotation, Hand.transform);
//         }
//     }

//     IEnumerator Example()
//     {
//         yield return new WaitForSeconds(0.5f);

//         Clones = GameObject.FindGameObjectsWithTag("Clone");

//         foreach (GameObject Clone in Clones)
//         {
//             Destroy(Clone);
//         }
        
//     }

//     public void Shuffle()
//     {
//         for (int i = 0; i < deckSize; i++)
//         {
//             container[0] = deck[i];
//             int randomIndex = Random.Range(i, deckSize);
//             deck[i] = deck[randomIndex];
//             deck[randomIndex] = container[0];
//         }
//         Instantiate(CardBack, transform.position, transform.rotation);
//         StartCoroutine(Example());
//     }

//     IEnumerator Draw(int x)
//     {
//         for (int i = 0; i < x; i++)
//         {
//             yield return new WaitForSeconds(1f);
//             SoundManager.PlaySound(SoundType.Draw);
//             Instantiate(CardToHand, transform.position, transform.rotation, Hand.transform);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();

    public static int deckSize;
    public TextMeshProUGUI deckSizeText;

    public GameObject cardIndex1;
    public GameObject cardIndex2;
    public GameObject cardIndex3;
    public GameObject cardIndex4;
    public GameObject cardIndex5;
    public GameObject cardIndex6;
    public GameObject cardIndex7;
    public GameObject CardToHand;
    public GameObject CardBack;
    public GameObject Deck;
    public GameObject[] Clones;
    public GameObject Hand;

    public TextMeshProUGUI loseText;
    public GameObject LoseTextGameObject;

    void Awake()
    {
        // Shuffle sẽ được gọi sau khi deck đã được tạo từ dữ liệu PlayFab ở Start()
    }

    void Start()
    {
        deck.Clear();
        int[] deckData = DeckCreator.lastDeckLoaded; // phải đảm bảo DeckCreator.lastDeckLoaded đã được set trước khi chuyển scene!

        if (deckData != null && deckData.Length > 0)
        {
            for (int i = 1; i < deckData.Length; i++)
            {
                for (int j = 0; j < deckData[i]; j++)
                {
                    deck.Add(CardDatabase.cardList[i]);
                }
            }
            deckSize = deck.Count;
        }
        else
        {
            Debug.LogWarning("Deck data not loaded from PlayFab! Using empty deck.");
            deckSize = 0;
        }

        // Chuẩn bị container cho shuffle (phải cùng size)
        container = new List<Card>(deck.Count);
        for (int i = 0; i < deck.Count; i++) container.Add(null);

        Shuffle();
    }

    void Update()
    {
        if (deckSize <= 0)
        {
            LoseTextGameObject.SetActive(true);
            loseText.text = "YOU LOSE";
        }
        int handSize = Hand.transform.childCount;

        staticDeck = deck;

        if (deckSize < 30 && cardIndex1) cardIndex1.SetActive(false);
        if (deckSize < 20 && cardIndex2) cardIndex2.SetActive(false);
        if (deckSize < 10 && cardIndex3) cardIndex3.SetActive(false);
        if (deckSize < 5  && cardIndex4) cardIndex4.SetActive(false);
        if (deckSize < 3  && cardIndex5) cardIndex5.SetActive(false);
        if (deckSize < 2  && cardIndex6) cardIndex6.SetActive(false);
        if (deckSize < 1  && cardIndex7) cardIndex7.SetActive(false);

        if (ThisCard.drawX > 0)
        {
            StartCoroutine(Draw(ThisCard.drawX));
            ThisCard.drawX = 0;
        }

        if (TurnSystem.startTurn == true && TurnSystem.isYourTurn)
        {
            if (handSize < 5)
            {
                StartCoroutine(Draw(1));
            }
            TurnSystem.startTurn = false;
        }

        if (deckSizeText != null)
            deckSizeText.text = "Deck: " + deckSize;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(CardToHand, transform.position, transform.rotation, Hand.transform);
        }
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(0.5f);

        Clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (GameObject Clone in Clones)
        {
            Destroy(Clone);
        }
    }

    public void Shuffle()
    {
        if (deck.Count == 0) return;
        for (int i = 0; i < deck.Count; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
        if (CardBack != null)
            Instantiate(CardBack, transform.position, transform.rotation);
        StartCoroutine(Example());
    }

    IEnumerator Draw(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.PlaySound(SoundType.Draw);
            Instantiate(CardToHand, transform.position, transform.rotation, Hand.transform);
        }
    }
}
