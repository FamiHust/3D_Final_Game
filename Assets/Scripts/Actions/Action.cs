using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.GameStates
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(float d);
    }
}
