using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.GameElements
{
    public abstract class GE_Logic : ScriptableObject
    {
        public abstract void OnClick(CardInstance inst);
        public abstract void OnHighLight(CardInstance inst);
    }
}
