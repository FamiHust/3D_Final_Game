using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();

    public static int deckSize;
    private bool hasInitialDraw = false;
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

    void Start()
    {
        deck.Clear();
        hasInitialDraw = false;

        int[] deckData = DeckCreator.lastDeckLoaded;

        if (deckData == null || deckData.Length == 0)
        {
            string json = PlayerPrefs.GetString("DeckData", "");
            if (!string.IsNullOrEmpty(json))
            {
                deckData = JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        if (deckData != null && deckData.Length > 0)
        {
            for (int i = 0; i < deckData.Length; i++)
            {
                for (int j = 0; j < deckData[i]; j++)
                {
                    if (i >= 0 && i < CardDatabase.cardList.Count)
                        deck.Add(CardDatabase.cardList[i]);
                    else
                        Debug.LogWarning($"Card index {i} out of range!");
                }
            }
            deckSize = deck.Count;
        }
        else
        {
            deckSize = 0;
        }

        container = new List<Card>(deck.Count);
        for (int i = 0; i < deck.Count; i++) container.Add(null);

        Shuffle();
    }


    void Update()
    {
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
            if (handSize < 5 && hasInitialDraw)
            {
                StartCoroutine(Draw(1));
            }
            TurnSystem.startTurn = false;
        }

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
        hasInitialDraw = true;
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
