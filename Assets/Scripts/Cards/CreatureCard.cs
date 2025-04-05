using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Cards/Creature")]
    public class CreatureCard : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);

            viz.statsHolder.SetActive(true);
        }
    }
}