using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
using TMPro;
using SO.UI;

namespace SO
{
    public class UpdateTextFromPhase : UIPropertyUpdater
    {
        public PhaseVariable currentPhase;
        public TextMeshProUGUI targetText;
        
        /// <summary>
        /// Use this to update a text UI element based on the target string variable
        /// </summary>
        public override void Raise()
        {
            targetText.text = currentPhase.value.phaseName;
        }
    }
}
