using UnityEngine;
using System;

namespace NumericalControl
{
    [Serializable]
    public class ToggleAndLigth
    {
        [SerializeField]
        private GameObject toggle;
        [SerializeField]
        private GameObject ligth;
        private Toggle _componentToggle;
        private LightEmittingDiode _componentLightEmittingDiode;

        internal void InitStart()
        {
            _componentToggle = toggle.GetComponent<ToggleSwitch>();
            _componentLightEmittingDiode = ligth.GetComponent<LightEmittingDiode>();
            InitEvents();
        }

        private void InitEvents()
        {
            _componentToggle.EventChangeState += ChengeToggleMode;
        }

        private void ChengeToggleMode(StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle)
        {
            _componentLightEmittingDiode.ChangeState(newState);
        }
    }
}