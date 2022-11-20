using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NumericalControl
{
    internal enum ElementsInterface { Tooltip, Try, Time, MiddleMassege , MiddleMassegeLeft, MiddleMassegeRigth}
    internal class InterfecControler : MonoBehaviour
    {
        [SerializeField]
        private ElementInterface tooltipObject; 
        [SerializeField]
        private ElementInterface tryObject; 
        [SerializeField]
        private ElementInterface timeObject;
        [SerializeField]
        private ElementInterface middleMassegeObject;
        [SerializeField]
        private TextMeshProUGUI middleMassegeLeftObject;
        [SerializeField]
        private TextMeshProUGUI middleMassegeRigthObject;
        [SerializeField]
        internal GameObject buttonForResetGame; 
        private Dictionary<ElementsInterface, TextMeshProUGUI> _textElementsInterface;
        private Dictionary<ElementsInterface, GameObject> _imageElementsInterface;

        void Awake()
        {
            _textElementsInterface = new Dictionary<ElementsInterface, TextMeshProUGUI>
            {
                {ElementsInterface.Tooltip, tooltipObject.text.GetComponent<TextMeshProUGUI>()},
                {ElementsInterface.Try,  tryObject.text.GetComponent<TextMeshProUGUI>() },
                {ElementsInterface.Time, timeObject.text.GetComponent<TextMeshProUGUI>() },
                {ElementsInterface.MiddleMassege, middleMassegeObject.text.GetComponent<TextMeshProUGUI>() },
                {ElementsInterface.MiddleMassegeLeft, middleMassegeLeftObject },
                {ElementsInterface.MiddleMassegeRigth, middleMassegeRigthObject },
            };
            _imageElementsInterface = new Dictionary<ElementsInterface, GameObject>
            {
                {ElementsInterface.Tooltip, tooltipObject.image},
                {ElementsInterface.Try,  tryObject.image },
                {ElementsInterface.Time, timeObject.image },
                {ElementsInterface.MiddleMassege, middleMassegeObject.image },
            };
        }

        internal void NewMassege(ElementsInterface elementInterface, string massege)
        {
            _textElementsInterface[elementInterface].text = massege;
        }

        internal void SetActiveMassege(ElementsInterface elementInterface, bool acrive) 
        {
            _imageElementsInterface[elementInterface].SetActive(acrive);
        }
    }
}
