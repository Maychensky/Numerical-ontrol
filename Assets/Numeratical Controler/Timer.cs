using System;
using UnityEngine;
using System.Collections;

/* создан для более удобно ввода через инспектор */
namespace NumericalControl  
{
    internal class Timer : MonoBehaviour
    {
        private const int SECONDS_IN_HOUR = 3600;
        private const int SECONDS_IN_MINUTE = 60;
        [SerializeField]
        internal int hours;
        [SerializeField]
        internal int minutes;
        [SerializeField]
        internal int seconds;
        internal int _remainingHours;
        internal int _remainingMinutes;
        internal int _remainingSeconds;
        private float _allReminsSeconds;
        private float _allSeconds;
        private float _secondsStepTimer = 1f;
        internal event Action<int, int, int> PassedStepTimer;
        internal event Action EndTimer;

        internal NumericalControl.Time GetRemainsTime()
        {
            return ConvertInTime(_allSeconds - _allReminsSeconds);
        }

        internal void ResetTimer()
        {
            SetValueTimer(0, 0, (int)_allSeconds);
        }

        internal void SetValueTimer(int hours, int minutes, int seconds)
        {
            _allReminsSeconds =  (hours * SECONDS_IN_HOUR) + (minutes * SECONDS_IN_MINUTE) + (seconds);
            _allSeconds = _allReminsSeconds;
            CorrectTime();
        }

        internal void StartTimer()
        {
            StartCoroutine(StartCorutioneTimer());
        }

        private IEnumerator StartCorutioneTimer()
        {
            while (_allReminsSeconds > 0)
            {
                yield return new WaitForSecondsRealtime(_secondsStepTimer);
                CorrectTime();
                PassedStepTimer?.Invoke(_remainingHours, _remainingMinutes, _remainingSeconds);
            }
            EndTimer?.Invoke();
        }

        private void CorrectTime()
        {
            _allReminsSeconds -= _secondsStepTimer;
            ConvertInTime();
        }

        // todo: добавленна для меньшего расхода пямяти
        private void ConvertInTime()
        {
            _remainingHours = (int)_allReminsSeconds / SECONDS_IN_HOUR;
            _remainingMinutes = (int)_allReminsSeconds / SECONDS_IN_MINUTE;
            _remainingSeconds = (int)_allReminsSeconds % SECONDS_IN_MINUTE;
        }

        private NumericalControl.Time ConvertInTime(float secondsValue)
        {
            NumericalControl.Time time = new NumericalControl.Time();
            time.hours = (int)secondsValue / SECONDS_IN_HOUR;
            time.minutes = (int)secondsValue / SECONDS_IN_MINUTE;
            time.seconds = (int)secondsValue % SECONDS_IN_MINUTE;
            return time;
        }
    }
}