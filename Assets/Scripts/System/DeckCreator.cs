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
    public int maxCards = 40;
    public Text cardCountText;

    // Start is called before the first frame update
    void Start()
    {
        sum = 0;
        cardCountText.text = $"Bộ bài: {sum}/{maxCards}";
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

    // public void Drop()
    // {
    //     if (mouseOverDeck == true && coll.GetComponent<Collection>().HowManyCards[dragged] > 0)
    //     {
    //         cardsWithThisID[dragged]++;

    //         if (cardsWithThisID[dragged] < 0)
    //         {
    //             cardsWithThisID[dragged] = 0;
    //         }

    //         coll.GetComponent<Collection>().HowManyCards[dragged]--;

    //         CalculateDrop();
    //         UpdateCardCountDisplay();

    //         SoundManager.PlaySound(SoundType.Drop);
    //     }
    // }
    public void Drop()
    {
        if (mouseOverDeck && coll.GetComponent<Collection>().HowManyCards[dragged] > 0)
        {
            // Tính tổng hiện tại
            int currentTotal = 0;
            for (int i = 0; i < numberOfCardsInDatabase; i++)
            {
                currentTotal += cardsWithThisID[i];
            }

            // Nếu đã đủ 40 lá, không thêm nữa
            if (currentTotal >= maxCards)
            {
                Debug.Log("Đã đủ 40 lá, không thể thêm nữa.");
                return;
            }

            // Thêm bài vào deck
            cardsWithThisID[dragged]++;

            if (cardsWithThisID[dragged] < 0)
            {
                cardsWithThisID[dragged] = 0;
            }

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

    public void CreateDeck()
    {
        sum = 0;
        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            sum += cardsWithThisID[i];
        }

        if (sum == 40)
        {
            for (int i = 0; i < numberOfCardsInDatabase; i++)
            {
                PlayerPrefs.SetInt("Bộ bài:" + i, cardsWithThisID[i]);
            }
        }

        sum = 0;
        numberOfDifferentCards = 0;

        for (int i = 0; i < numberOfCardsInDatabase; i++)
        {
            saveDeck[i] = PlayerPrefs.GetInt("Bộ bài:" + i, 0);
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

            // Cập nhật text hiển thị
            UpdateCardCountDisplay();

            // Nếu quantity về 0 -> hủy object trong deck
            if (quantity[id] <= 0)
            {
                alreadyCreated[id] = false;

                // Tìm all WindowInDeck trong scene, xóa đúng cái đang giữ ID này
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
