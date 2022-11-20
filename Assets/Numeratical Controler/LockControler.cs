using System.Collections;
using UnityEngine;

namespace NumericalControl
{
    internal class LockControler : Toggle
    {
        private const int CORYTINE_ITERATIONS = 20;
        [SerializeField]
        private GameObject keyObject;
        [SerializeField]
        private GameObject handleKeyObject;
        [SerializeField]
        private Axis axisKey = Axis.Y;
        [SerializeField]
        private Axis axisKeyRotation = Axis.Y;
        [SerializeField]
        [Range(0, 100f)]
        private float shiftPositionKey = 1f;
        [SerializeField]
        [Range(0,5f)]
        private float timeAutoPress = 1f;
        private float _angleStepForROtate;
        private Vector3 _startPositionKey;
        private bool _checkEndCorutine;
        
        void Awake()
        {
            _checkEndCorutine = true;
            state = StateToggle.Inactive;
            _angleStepForROtate = 180 / 20;
        }

        void Start()
        {
            _startPositionKey = keyObject.transform.position;
        } 

        internal override void ChangeState(StateToggle stateToggle)
        {
            if (stateToggle == StateToggle.Inactive) StartCoroutine(AnimationKeyOff());
            else StartCoroutine(AnimationKeyOn());
            state = stateToggle;
        } 
        
        private bool GetEndCorutine() 
        {
            if (_checkEndCorutine == true)
            {
                _checkEndCorutine = false;
                return true;
            }
            return _checkEndCorutine;
        }

        // todo: сделать корутины читабальными 
        private IEnumerator AnimationKeyOn()
        {
            yield return new WaitUntil(GetEndCorutine);
            Vector3 vectorToLock = transform.position - keyObject.transform.position;
            float iterationStepPosition = vectorToLock.GetAxis(axisKey) / (CORYTINE_ITERATIONS);
            for (int i = 0; i < CORYTINE_ITERATIONS; i ++)
            {
                yield return new WaitForSeconds(timeAutoPress / CORYTINE_ITERATIONS);
                keyObject.transform.position += _axisUnitVector[axisKey] * iterationStepPosition;
            }
            for (int i = 0; i < CORYTINE_ITERATIONS; i ++)
            {
                yield return new WaitForSeconds(timeAutoPress / CORYTINE_ITERATIONS);
                handleKeyObject.transform.Rotate(new Vector3(-_angleStepForROtate,0,0)); 
            }
            _checkEndCorutine = true;
        } 

        private IEnumerator AnimationKeyOff()
        {
            yield return new WaitUntil(GetEndCorutine);
            Vector3 vectorToStartPositionKey = _startPositionKey - keyObject.transform.position;
            float iterationStepPosition = vectorToStartPositionKey.GetAxis(axisKey) / (CORYTINE_ITERATIONS);
            for (int i = 0; i < CORYTINE_ITERATIONS; i ++)
            {
                yield return new WaitForSeconds(timeAutoPress / CORYTINE_ITERATIONS);
                handleKeyObject.transform.Rotate(new Vector3(_angleStepForROtate,0,0)); 
            }
            for (int i = 0; i < CORYTINE_ITERATIONS; i ++)
            {
                yield return new WaitForSeconds(timeAutoPress / CORYTINE_ITERATIONS);
                keyObject.transform.position += _axisUnitVector[axisKey] * iterationStepPosition;
            }
            _checkEndCorutine = true;
        } 
    }
}
