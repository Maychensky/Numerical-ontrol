using System;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

namespace NumericalControl
{
    [Serializable]
    internal class ElementInterface
    {
        [SerializeField]
        internal GameObject image;
        [SerializeField]
        internal GameObject text;

        internal void CheckComponnts ()
        {
            if (!(image.GetComponent<Image>() && text.GetComponent<TextMeshProUGUI>()))
                throw new Exception("No component");
        }
    }
}