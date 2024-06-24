using System.Collections;
using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Game
{
    public enum GameStates
    {
        IDLE,
        RUNNING,
        LOST,
        ENDED,
    }
    
    public class RunningGameViewScreen : ScreenBase<IGamePresenter>
    {
        private const float LIGHT_ON_INTERVAL = 0.8f;

        [SerializeField] private GameObject[] lightStrips;
        [SerializeField] private float minDelay = 1.2f;
        [SerializeField] private float maxDelay = 2.5f;
        
        [SerializeField] private Button runningGameButton;
        
        private IGamePresenter presenter;
        private Coroutine runningCoroutine;
        private GameStates currentState = GameStates.IDLE;


        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            currentState = GameStates.RUNNING;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            runningGameButton.onClick.AddListener(OnRunningGame);
            runningCoroutine = StartCoroutine(StartLights());
        }
        
        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            if (presenter is null) return;
            runningGameButton.onClick.RemoveListener(OnRunningGame);
        }

        private void OnRunningGame()
        {
            switch (currentState)
            {
                case GameStates.RUNNING:
                    ClearLights();
                    StopCoroutine(runningCoroutine);
                    Hide();
                    presenter.StopGame();
                    break;
                case GameStates.ENDED:
                    ClearLights();
                    StopCoroutine(runningCoroutine);
                    Hide();
                    presenter.EndGame();
                    break;
            }
        }
        
        private IEnumerator StartLights()
        {
            foreach (var lightStrip in lightStrips)
            {
                lightStrip.SetActive(true);
                yield return new WaitForSeconds(LIGHT_ON_INTERVAL);
            }
            
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            ClearLights();
            presenter.SetStartTime(Time.time);
            currentState = GameStates.ENDED;
        }
        
        private void ClearLights()
        {
            foreach (GameObject panel in lightStrips)
            {
                panel.SetActive(false);
            }
        }
    }
}