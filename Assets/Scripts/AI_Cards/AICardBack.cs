using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardBack : MonoBehaviour
{
    [SerializeField] private GameObject Deck;
    [SerializeField] private GameObject It;

    void Update()
    {
        Deck = GameObject.Find("Deck_Panel_Enemy");
        It.transform.SetParent(Deck.transform);
        It.transform.localScale = Vector3.one;
        It.transform.localPosition = new Vector3(It.transform.localPosition.x, It.transform.localPosition.y, 0f);
        It.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
