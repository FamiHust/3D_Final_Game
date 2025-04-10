using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBack : MonoBehaviour
{
        public GameObject cardBack;
        public ThisCard thisCardScript;

        void Update()
        {
            if (thisCardScript.cardBack)
            {
                cardBack.SetActive(true);
            }
            else
            {
                cardBack.SetActive(false);
            }
        }
    
}
