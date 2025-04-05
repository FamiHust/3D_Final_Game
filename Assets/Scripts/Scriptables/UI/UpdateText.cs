﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using TMPro;

namespace SO.UI
{
    public class UpdateText : UIPropertyUpdater
    {
        public StringVariable targetString;
        public TextMeshProUGUI targetText;
        
        /// <summary>
        /// Use this to update a text UI element based on the target string variable
        /// </summary>
        public override void Raise()
        {
            targetText.text = targetString.value;
        }
        
        public void Raise(string target)
        {
            targetText.text = target;
        }
    }
}
