using UnityEngine;
using System.Collections.Generic;
using System;

namespace NumericalControl
{
    internal enum StateToggle
    {
        Inactive,
        Neutral,
        Active,
    }

    // класс реализует переключатель, рассчитан для наследования
    // измения в инспекторе расчитаны только для в режима сцены
    // учтены возможные изменения компонента transfor в режиме игры 
    public abstract class Toggle : MonoBehaviour
    {
        [SerializeField]
        internal ElementsNumericalControl nameToggle;
        internal delegate void Action (StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle);
        internal event Action EventChangeState;

        [SerializeField]
        internal StateToggle state = StateToggle.Neutral;
        internal readonly Dictionary<Axis, Vector3> _axisUnitVector = new Dictionary<Axis, Vector3>()
        {
            {Axis.X, Vector3.right},
            {Axis.Y, Vector3.up},
            {Axis.Z, Vector3.forward},
        };

        void OnMouseDown()
        {
            NextState();
        }

        internal void NextState()
        {
            StateToggle oldState = state;
            if (state == StateToggle.Inactive) ChangeState(StateToggle.Active);
            else if (state == StateToggle.Active) ChangeState(StateToggle.Inactive);
            else
            {
                ChangeState(StateToggle.Inactive);
                state = StateToggle.Inactive;
            } 
            if (state != StateToggle.Neutral)
                EventChangeState?.Invoke(oldState, state, nameToggle);
        }
        internal virtual void ChangeState (StateToggle stateToggle) { }
    }
}