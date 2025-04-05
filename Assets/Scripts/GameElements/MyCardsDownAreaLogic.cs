// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace SA
// {
//     [CreateAssetMenu(menuName = "Areas/MyCardsDownWhenHoldingCard")]
//     public class MyCardsDownAreaLogic : AreaLogic
//     {
//         public CardVariable card;
//         public CardType creatureType;
//         public SO.TransformVariable areaGrid;

//         public override void Execute()
//         {
//             if (card.value == null) return;

//             if (card.value.viz.card.cardType == creatureType)
//             {
//                 card.value.transform.SetParent(areaGrid.value.transform);
//                 card.value.transform.localPosition = Vector3.zero;
//                 card.value.transform.localScale = Vector3.one;
//                 card.value.transform.localEulerAngles = Vector3.zero;

//                 card.value.gameObject.SetActive(true);
//             }
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using SA.GameElements;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Areas/MyCardsDownWhenHoldingCard")]
    public class MyCardsDownAreaLogic : AreaLogic
    {
        public CardVariable card;
        public CardType creatureType;
        public SO.TransformVariable areaGrid;
        public GameElements.GE_Logic cardDownLogic;

        public override void Execute()
        {
            if (card.value == null) return;

            if (card.value.viz.card.cardType == creatureType)
            {
                Settings.SetParentForCard(card.value.transform, areaGrid.value.transform);
                card.value.gameObject.SetActive(true);
                card.value.currentLogic = cardDownLogic;

                // Kiểm tra và xóa script cardWrapper
                var cardWrapper = card.value.GetComponent<CardWrapper>();
                if (cardWrapper != null)
                {
                    Destroy(cardWrapper);
                }
            }
        }
    }
}
