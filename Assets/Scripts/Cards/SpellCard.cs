using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Cards/Spell")]
    public class SpellCard : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);

            viz.statsHolder.SetActive(false);
        }
    }
}
