using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.GameElements
{
    [CreateAssetMenu(menuName = "Game Elements/My Card Down")]
    public class MyCardDown : GE_Logic
    {
        public override void OnClick(CardInstance inst)
        {
            Debug.Log("This card is mine, but it's on the table");
        }

        public override void OnHighLight(CardInstance inst)
        {

        }
    }
}

