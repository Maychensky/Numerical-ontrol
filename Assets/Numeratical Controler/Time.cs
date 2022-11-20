using System;
using UnityEngine;

namespace NumericalControl
{
    [Serializable]
    internal class Time
    {
        [SerializeField]
        internal int hours;
        [SerializeField]
        internal int minutes;
        [SerializeField]
        internal int seconds;
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", hours, minutes,seconds);
        }
    }
}