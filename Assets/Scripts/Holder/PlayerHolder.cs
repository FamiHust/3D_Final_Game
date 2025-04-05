using System.Collections;
using System.Collections.Generic;
using SA.GameElements;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Areas/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string[] startingCards;
        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;

        public GE_Logic handLogic;
        public GE_Logic downLogic;
        
        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();

    }
}
