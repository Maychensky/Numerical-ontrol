using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace NumericalControl
{
    internal sealed class ButtonSwitch: Toggle
    {
        [SerializeField]
        private Axis axisPressing = Axis.X; 
        [SerializeField]
        private DirectionVector axisDirectionPressing = DirectionVector.Forward;
        [SerializeField]
        [Range(0f, 1f)]
        private float depthPressingPercent = 0.5f;
        [SerializeField]
        private bool hasAutoPress;
        [SerializeField]
        private float timeAutoPress = 0.5f;
        private float _depthPressing; 
        private Dictionary< StateToggle, float> _depthPressingStates; 

        void Awake()
        {
            state = StateToggle.Inactive;
        }

        void Start()
        {
            InitSizeDepthPressing();
            InitDepthPressingStates();
        }

        private void InitSizeDepthPressing()
        {
            int directionalMultiplier = (axisDirectionPressing == DirectionVector.Forward) ? 1 : -1;
            _depthPressing = transform.localScale.GetAxis(axisPressing) * depthPressingPercent * directionalMultiplier;
        }
        
        private void InitDepthPressingStates()
        {
            _depthPressingStates = new Dictionary<StateToggle, float>
            { 
                {StateToggle.Inactive, -_depthPressing},
                {StateToggle.Active, _depthPressing},
            };
        }

        internal override void ChangeState(StateToggle stateToggle)
        {
            CorrectPosition(stateToggle);
            if (hasAutoPress && state == StateToggle.Inactive)
                StartCoroutine(AutoPress());
            state = stateToggle;
        }

        private void CorrectPosition(StateToggle stateToggle)
        {
            transform.position += GetShiftPosition(stateToggle);
        }

        private Vector3 GetShiftPosition(StateToggle stateToggle)
        {
            float shiftPosition = _depthPressingStates[stateToggle] - _depthPressingStates[state];
            return _axisUnitVector[axisPressing.PreviousAxis()] * shiftPosition;
        }

        private IEnumerator AutoPress()
        {
            yield return new WaitForSeconds(timeAutoPress);
            ChangeState(StateToggle.Inactive);
        }
    }
}