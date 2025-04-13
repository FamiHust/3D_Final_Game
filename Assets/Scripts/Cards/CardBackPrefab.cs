using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackPrefab : MonoBehaviour
{
    [SerializeField] private GameObject Deck;
    [SerializeField] private GameObject It;
    // Update is called once per frame
    void Update()
    {
        Deck = GameObject.Find("Deck_Panel");
        It.transform.SetParent(Deck.transform);
        It.transform.localScale = Vector3.one;
        It.transform.localPosition = new Vector3(It.transform.localPosition.x, It.transform.localPosition.y, 0f);

        It.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
