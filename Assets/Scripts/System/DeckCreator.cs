using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCreator : MonoBehaviour
{
    public int[] cardsWithThisID;
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

    // Start is called before the first frame update
    void Start()
    {
        sum = 0;
        numberOfCardsInDatabase = 32;

    }

    private void Update() 
    {

    }

    public void CreateDeck()
    {
        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            sum += cardsWithThisID[i];
        }

        if (sum == 40)
        {
            for (int i=0; i < numberOfCardsInDatabase; i++)
            {
                PlayerPrefs.SetInt("deck" + i, cardsWithThisID[i]);
            }
        }
        sum = 0;
        numberOfDifferentCards = 0;

        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            saveDeck[i] = PlayerPrefs.GetInt("deck" + i, 0);
        }
    }

    public void EnterDeck()
    {
        mouseOverDeck = true;
    }

    public void ExitDeck()
    {
        mouseOverDeck = false;
    }

    public void Card1()
    {
        dragged = Collection.x;
    }
    public void Card2()
    {
        dragged = Collection.x+1;
    }
    public void Card3()
    {
        dragged = Collection.x+2;
    }
    public void Card4()
    {
        dragged = Collection.x+3;
    }
    public void Card5()
    {
        dragged = Collection.x+4;
    }
    public void Card6()
    {
        dragged = Collection.x+5;
    }
    public void Card7()
    {
        dragged = Collection.x+6;
    }
    public void Card8()
    {
        dragged = Collection.x+7;
    }
    public void Card9()
    {
        dragged = Collection.x+8;
    }
    public void Card10()
    {
        dragged = Collection.x+9;
    }
    public void Card11()
    {
        dragged = Collection.x+10;
    }
    public void Card12()
    {
        dragged = Collection.x+11;
    }

    public void Drop()
    {
        if (mouseOverDeck == true && coll.GetComponent<Collection>().HowManyCards[dragged] > 0)
        {
            cardsWithThisID[dragged]++;

            if (cardsWithThisID[dragged] < 0)
            {
                cardsWithThisID[dragged] = 0;
            }

            coll.GetComponent<Collection>().HowManyCards[dragged]--;

            CalculateDrop();
        }
    }

    public void CalculateDrop()
    {
        lastAdded = 0;
        int i = dragged;

        if (cardsWithThisID[i] > 0 && alreadyCreated[i] == false)
        {
            lastAdded =i;
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
            alreadyCreated[i] = true;

            quantity[i] = 1;
        }
        else if (cardsWithThisID[i] > 0 && alreadyCreated[i] == true)
        {
            quantity[i] ++;
        }
    }
}
