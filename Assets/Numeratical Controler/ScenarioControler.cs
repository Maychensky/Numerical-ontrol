using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumericalControl
{
    public class ScenarioControler : MonoBehaviour
    {
        [SerializeField]
        private InterfecControler interfecControler;
        [SerializeField]
        private Comands comands;
        [SerializeField]
        private ElementsNCControler _elementsNC;
        private ElementsComand _curretTask;
        private int _indexElement; 
        private bool _scenarioActive;
        private int _numberNegativeTry;
        internal delegate void ActionNegativeScenario (StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle, int numberNegativeTry);
        internal delegate void ActionScenarioPassed ();
        internal event ActionNegativeScenario eventNegativeScenario;
        internal event ActionScenarioPassed eventScenarioPassed;

        void Start()
        {
            InitChangeState();
        }

        internal void StartScenatio()
        {
            _scenarioActive = true;
            _indexElement = 0;
            _numberNegativeTry = 0;
            _curretTask = NextComand();
        }

        internal void StopScenatio()
        {
            _indexElement = 0;
            _numberNegativeTry = 0;
            _scenarioActive = false;
        }
        
        internal void StartInoreScenario() => _scenarioActive = false;
        internal void StopIgnoreScenario() => _scenarioActive = true;

        internal void TestAction(StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle)
        {
            if (_scenarioActive)
                if (_curretTask.elementsNumericalControl == nameToggle && _curretTask.nextState == newState) _curretTask = NextComand();
                else eventNegativeScenario?.Invoke(oldState, newState, nameToggle, ++_numberNegativeTry);
        }

        private ElementsComand NextComand()
        {
            if (_indexElement < comands.elementsComand.Length)
                OutTooltip(comands.elementsComand[_indexElement].text);
            if (_indexElement == comands.elementsComand.Length)
            {
                eventScenarioPassed?.Invoke();
                _scenarioActive = false;
                return comands.elementsComand[0];
            } 
            else  return comands.elementsComand[_indexElement ++ ];
        }

        private void OutTooltip(string tooltip)
        {
            interfecControler.NewMassege(ElementsInterface.Tooltip ,tooltip);
        }

        private void InitChangeState()
        {
            _elementsNC.button1.GetComponent<ButtonSwitch>().EventChangeState += TestAction;
            _elementsNC.button2.GetComponent<ButtonSwitch>().EventChangeState += TestAction;
            _elementsNC.toggle1.GetComponent<ToggleSwitch>().EventChangeState += TestAction;
            _elementsNC.toggle2.GetComponent<ToggleSwitch>().EventChangeState += TestAction;
            _elementsNC.locked.GetComponent<LockControler>().EventChangeState += TestAction;
        }
    }
}
