using System;
using UnityEngine;
using System.Collections.Generic;

namespace NumericalControl
{
    internal enum ElementsNumericalControl { Button1, Button2, Toggle1, Toggle2, Ligth1, Ligth2, Locked, }
    internal class ElementsNCControler : MonoBehaviour
    {
        [SerializeField]
        internal GameObject button1;
        [SerializeField]
        internal GameObject button2;
        [SerializeField]
        internal GameObject toggle1;
        [SerializeField]
        internal GameObject toggle2;
        [SerializeField]
        internal GameObject ligth1;
        [SerializeField]
        internal GameObject ligth2;
        [SerializeField]
        internal GameObject locked; 

        private Dictionary<ElementsNumericalControl, GameObject> _dictionaryElementsNumericalControl;
        private Dictionary<ToggleSwitch, LightEmittingDiode> _dictionaryToggleAndLingth;
         private Dictionary<ElementsNumericalControl, Type> _childsToggle = new Dictionary<ElementsNumericalControl, Type>
        {
            {ElementsNumericalControl.Button1, typeof(ButtonSwitch)},
            {ElementsNumericalControl.Button2, typeof(ButtonSwitch)},
            {ElementsNumericalControl.Toggle1, typeof(ToggleSwitch)},
            {ElementsNumericalControl.Toggle2, typeof(ToggleSwitch)},
            {ElementsNumericalControl.Locked, typeof(LockControler)},
        };

        void Awake()
        {
            _dictionaryElementsNumericalControl = new Dictionary<ElementsNumericalControl, GameObject>
            {
                {ElementsNumericalControl.Button1, button1},
                {ElementsNumericalControl.Button2, button2},
                {ElementsNumericalControl.Toggle1, toggle1},
                {ElementsNumericalControl.Toggle2, toggle2},
                {ElementsNumericalControl.Ligth1, ligth1},
                {ElementsNumericalControl.Ligth2, ligth2},
                {ElementsNumericalControl.Locked, locked},
            };
            _dictionaryToggleAndLingth = new Dictionary<ToggleSwitch, LightEmittingDiode>
            {
                {toggle1.GetComponent<ToggleSwitch>(), ligth1.GetComponent<LightEmittingDiode>()},
                {toggle2.GetComponent<ToggleSwitch>(), ligth2.GetComponent<LightEmittingDiode>()}
            };
        }
        void Start()
        {
            locked.GetComponent<LockControler>().EventChangeState += CorrectActiveLinthsForLock;
            toggle1.GetComponent<ToggleSwitch>().EventChangeState += CorrectActiveLinthsForLock;
            toggle2.GetComponent<ToggleSwitch>().EventChangeState += CorrectActiveLinthsForLock;
        }

        private void CorrectActiveLinthsForLock(StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle)
        {
            CorrectActiveLinths(); 
        }

        internal GameObject GetElement(ElementsNumericalControl nameElement) 
        {
            return _dictionaryElementsNumericalControl[nameElement];
        }

        internal void ChangeModes(Comands chengableModes)
        {
            foreach (ElementsComand modeForElementNC in chengableModes.elementsComand)
            {
                GameObject curretToggle = GetElement(modeForElementNC.elementsNumericalControl);
                Type typeToggle = _childsToggle[modeForElementNC.elementsNumericalControl];
                Toggle curretComponentToggle = (Toggle)curretToggle.GetComponent(typeToggle);
                if (curretComponentToggle.state != modeForElementNC.nextState)
                    curretComponentToggle.ChangeState(modeForElementNC.nextState);
                ChengeLigth(curretComponentToggle, modeForElementNC.nextState, typeToggle);
            }
            CorrectActiveLinths();
        }

        private void ChengeLigth(Toggle componentToggle, StateToggle curretState, Type curretType)
        {
            if (curretType == typeof(ToggleSwitch))
            {
                _dictionaryToggleAndLingth[(ToggleSwitch)componentToggle].ChangeState(curretState);
            }
                
        }

        internal Toggle GetComponentToggle(ElementsNumericalControl nameElement)
        {
            GameObject curretToggle = GetElement(nameElement);
            Type typeToggle = _childsToggle[nameElement];
            return (Toggle)curretToggle.GetComponent(typeToggle);
        }

        internal void CorrectActiveLinths()
        {
            if (locked.GetComponent<LockControler>().state != StateToggle.Active)
            {
                ligth1.GetComponent<LightEmittingDiode>().ChangeState(StateToggle.Neutral);
                ligth2.GetComponent<LightEmittingDiode>().ChangeState(StateToggle.Neutral);
            } 
            else 
            {
                ligth1.GetComponent<LightEmittingDiode>().ChangeState(toggle1.GetComponent<ToggleSwitch>().state);
                ligth2.GetComponent<LightEmittingDiode>().ChangeState(toggle2.GetComponent<ToggleSwitch>().state);
            }
        }
    }
}


