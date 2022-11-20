using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System.Collections;

namespace NumericalControl
{
    public class GameLogic : MonoBehaviour
    {
        private const int MAX_LENDTH_TIMER = 8;
        private const char TIME_SEPARATOR = ':';
        private const string MASSEGE_GAME_OVER = "GAME OVER";
        private const string MASSEGE_INCORRECT_TRY = "INCORRECT";
        private const string MASSEGE_WIN = "YOU WIN";
        private StringBuilder stringTimer;
        [SerializeField]
        NumericalControl.Time timeGame;
        [SerializeField]
        private int maxNumberNegativeTry = 3;
        [SerializeField]
        private ScenarioControler firstScenario;
        [SerializeField]
        private Comands startModesToggles;
        [SerializeField]
        private ElementsNCControler toggles;
        [SerializeField]
        private InterfecControler interfaceControler;
        private NumericalControl.Timer timer;
        private GameObject _timerGO;
        private int _remainsTry;
        private bool _isGameOver;
        void Start()
        {
            _remainsTry = maxNumberNegativeTry;
            stringTimer = new StringBuilder(MAX_LENDTH_TIMER);
            SetModesToggles(toggles, startModesToggles);
            StartScenarios(firstScenario);
            ChangeNumberTry(maxNumberNegativeTry);
            InitEvents();
            StartTimer();
        }

        private void SetModesToggles(ElementsNCControler elementsNumericalControl, Comands modes) => elementsNumericalControl.ChangeModes(modes);
        private void ChangeNumberTry(int numberRemainingTry) => interfaceControler.NewMassege(ElementsInterface.Try, numberRemainingTry.ToString());
        private void StartScenarios(ScenarioControler scenario) => scenario.StartScenatio();
        private void StopScenarios(ScenarioControler scenario) => scenario.StopScenatio();
    
        private void CheckOverflowTry(StateToggle oldState, StateToggle newState, ElementsNumericalControl nameToggle, int numberNegativeTry)
        {
            _remainsTry = maxNumberNegativeTry - numberNegativeTry;
            if (maxNumberNegativeTry == numberNegativeTry) GameOver();
            else ChangeNumberTry(_remainsTry);
            Toggle curretToggle =  toggles.GetComponentToggle(nameToggle);
            StepBack(curretToggle);
            StartCoroutine(MessageIncorrectTry(MASSEGE_INCORRECT_TRY));
        }
        private void InitEvents()
        {
            firstScenario.eventNegativeScenario += CheckOverflowTry;
            firstScenario.eventScenarioPassed += GameWin;
            interfaceControler.buttonForResetGame.GetComponent<Button>().onClick.AddListener(ResetGame);
        }

        private void StepBack(Toggle toggle)
        {
            firstScenario.StartInoreScenario();
            toggle.NextState();
            firstScenario.StopIgnoreScenario();
        }

        private void GameOver()
        {
            _isGameOver = true;
            interfaceControler.SetActiveMassege(ElementsInterface.MiddleMassege, true);
            interfaceControler.NewMassege(ElementsInterface.MiddleMassege, MASSEGE_GAME_OVER);
            SetActiveInterface(false);
        }

        private void GameWin()
        {
            _isGameOver = true;
            interfaceControler.SetActiveMassege(ElementsInterface.MiddleMassege, true);
            interfaceControler.NewMassege(ElementsInterface.MiddleMassege, MASSEGE_WIN);
            interfaceControler.NewMassege(ElementsInterface.MiddleMassegeLeft, timer.GetRemainsTime().ToString());
            interfaceControler.NewMassege(ElementsInterface.MiddleMassegeRigth, _remainsTry.ToString());
            SetActiveInterface(false);
        }

        private void ResetGame()
        {
            interfaceControler.SetActiveMassege(ElementsInterface.MiddleMassege, false);
            interfaceControler.NewMassege(ElementsInterface.MiddleMassegeRigth, "");
            interfaceControler.NewMassege(ElementsInterface.MiddleMassegeLeft, "");
            StopScenarios(firstScenario);
            SetModesToggles(toggles, startModesToggles);
            StartScenarios(firstScenario);
            ChangeNumberTry(maxNumberNegativeTry);
            timer.ResetTimer();
            _isGameOver = false;
            SetActiveInterface(true);
        }

        private void StartTimer()
        {
            _timerGO = new GameObject("timer", typeof(Timer));
            timer = _timerGO.GetComponent<Timer>();
            timer.PassedStepTimer += UpdateTimer;
            timer.EndTimer += GameOver;
            timer.SetValueTimer(timeGame.hours, timeGame.minutes, timeGame.seconds);
            timer.StartTimer();
        }

        private void UpdateTimer(int hours, int minutes, int seconds)
        {
            stringTimer.Clear();
            stringTimer.Append(hours.ToString("00"));
            stringTimer.Append(TIME_SEPARATOR);
            stringTimer.Append(minutes.ToString("00"));
            stringTimer.Append(TIME_SEPARATOR);
            stringTimer.Append(seconds.ToString("00"));
            interfaceControler.NewMassege(ElementsInterface.Time, stringTimer.ToString());
        }

        private IEnumerator MessageIncorrectTry(string massege)
        {
            if (!_isGameOver)
            {
                interfaceControler.SetActiveMassege(ElementsInterface.MiddleMassege, true);
                interfaceControler.NewMassege(ElementsInterface.MiddleMassege, massege);
                yield return new WaitForSeconds(2f);
                if (!_isGameOver)
                    interfaceControler.SetActiveMassege(ElementsInterface.MiddleMassege, false);
            }
        }

        private void SetActiveInterface(bool active)
        {
            interfaceControler.SetActiveMassege(ElementsInterface.Tooltip, active);
            interfaceControler.SetActiveMassege(ElementsInterface.Time, active);
            interfaceControler.SetActiveMassege(ElementsInterface.Try, active);
        }
    }
}
