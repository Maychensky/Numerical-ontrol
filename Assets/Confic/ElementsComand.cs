using UnityEngine;
using System;


namespace NumericalControl
{
    [Serializable]
    internal class ElementsComand
    {
        [SerializeField]
        internal ElementsNumericalControl elementsNumericalControl;
        [SerializeField]
        internal StateToggle nextState;
        [SerializeField]
        internal string text;
    }
}