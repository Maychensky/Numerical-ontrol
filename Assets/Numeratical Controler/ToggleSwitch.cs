using UnityEngine;
using UnityEditor;
using Unity;
using System.Collections.Generic;

namespace NumericalControl
{
    internal sealed class ToggleSwitch : Toggle
    {


        private const float MAX_ANGLE_RATATION = 90f;  

        [SerializeField]
        private Axis axisRotation = Axis.X; 
        [SerializeField]
        private Axis axisPosition = Axis.X; 
        [SerializeField]
        [Range(-MAX_ANGLE_RATATION, MAX_ANGLE_RATATION)]
        private float angleRotation = 20f; 
        private float _shiftForRotetion; 
        private Dictionary< StateToggle, float> _angleStates; 
        private Dictionary< StateToggle, float> _positionStates; 

        void Awake()
        {
            InitAngleStates();
            InitPositionStates();
        }
        void Start()
        {
            ChangeState(state);
        }

        private void InitAngleStates() 
        {
            _angleStates = new Dictionary<StateToggle, float>()
            {
                {StateToggle.Inactive, -angleRotation},
                {StateToggle.Neutral, 0f},
                {StateToggle.Active, angleRotation},
            };
        }
        
        private void InitPositionStates() 
        {
            InitShiftObjectForRotetion();
            _positionStates = new Dictionary<StateToggle, float>()
            {
                {StateToggle.Inactive, -_shiftForRotetion},
                {StateToggle.Neutral, 0f},
                {StateToggle.Active, _shiftForRotetion},
            };
        }

        private void InitShiftObjectForRotetion() 
        {
            Axis axisForSiftPosition = axisPosition;
            float hypotenuse = transform.localScale.GetAxis(axisForSiftPosition);
            float angleRotationRadian = angleRotation * (Mathf.PI / 180 );
            float sinAlpha = Mathf.Sin(angleRotationRadian);
            _shiftForRotetion = hypotenuse * sinAlpha * 2f;
        }

        internal override void ChangeState(StateToggle stateToggle)
        {
            CorrectRotation(stateToggle);
            CorrectPosition(stateToggle);
            state = stateToggle;
        }

        private void CorrectRotation(StateToggle stateToggle)
        {
            transform.rotation *= Quaternion.Euler(GetShiftAngle(stateToggle));
        }

        private void CorrectPosition(StateToggle stateToggle)
        {
            transform.position += GetShiftPosition(stateToggle);
        }

        private Vector3 GetShiftAngle(StateToggle stateToggle)
        {
            float shiftAngle = _angleStates[stateToggle] - _angleStates[state];
            return _axisUnitVector[axisRotation] * shiftAngle;
        }

        private Vector3 GetShiftPosition(StateToggle stateToggle)
        {
            float shiftPosition = _positionStates[stateToggle] - _positionStates[state];
            return _axisUnitVector[axisPosition] * shiftPosition;
        }
    }
}
