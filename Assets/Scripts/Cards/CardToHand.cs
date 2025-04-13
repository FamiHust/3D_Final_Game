using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject It;
    public int x;

    void Start()
    {
        Hand = GameObject.Find("My_Hands");
        It.transform.SetParent(Hand.transform);
        It.transform.localScale = Vector3.one;
        It.transform.localPosition = new Vector3(It.transform.localPosition.x, It.transform.localPosition.y, It.transform.localPosition.z);

        It.transform.eulerAngles = new Vector3(0, 0, 0);
        x = 1;
    }
}
