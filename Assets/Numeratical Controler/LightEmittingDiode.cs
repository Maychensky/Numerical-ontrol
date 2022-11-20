using UnityEngine;
using System.Collections.Generic;

namespace NumericalControl
{
    internal class LightEmittingDiode: MonoBehaviour
    {
        private MeshRenderer _componentMeshRender;
        private Dictionary<StateToggle, Color> _colorDiode;
        internal StateToggle state { get; private set;} 
        void Awake()
        {
            _colorDiode = new Dictionary<StateToggle, Color>
            {
                {StateToggle.Neutral, Color.gray},
                {StateToggle.Inactive, Color.red},
                {StateToggle.Active, Color.green},
            };
            _componentMeshRender = GetComponent<MeshRenderer>();
        }

        internal void ChangeState(StateToggle stateDiod)
        {
            state = stateDiod;
            ChengeColor();
        }

        private void ChengeColor()
        {
            _componentMeshRender.material.color = _colorDiode[state];
        }
    }
}