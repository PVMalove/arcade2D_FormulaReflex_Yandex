using System;
using System.Collections;
using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Screens.Game
{
    public enum GameStates
    {
        IDLE,
        RUNNING,
        WAITING
    }
    
    public class GameViewScreen : ScreenBase<IGamePresenter>
    {
        private const float LIGHT_ON_INTERVAL = 0.8f;

        [SerializeField] private GameObject[] lightStrips;
        [SerializeField] private float minDelay = 1.2f;
        [SerializeField] private float maxDelay = 2.5f;

        [SerializeField] private Text resultText;
        [SerializeField] private Text bestTimeText;

        [SerializeField] private Button startGameButton;
        [SerializeField] private Button openSkinsShopButton;
        
        private GameStates currentState = GameStates.IDLE;
        private Coroutine runningCoroutine;
        private float startTime;
        private float bestTime;

        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            bestTime = presenter.BestTime;
            UpdateBestTimeText();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
        }

        private void OnStartGame()
        {
            switch (currentState)
            {
                case GameStates.IDLE:
                    StartGame();
                    break;
                case GameStates.RUNNING:
                    StopGame();
                    break;
                case GameStates.WAITING:
                    EndGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartGame()
        {
            currentState = GameStates.RUNNING;
            runningCoroutine = StartCoroutine(StartLights());
        }
        
        private void StopGame()
        {
            ClearLights();
            StopCoroutine(runningCoroutine);
            resultText.text = "Jump Start!";
            currentState = GameStates.IDLE;
        }
        
        private void EndGame()
        {
            float timeDiff = Time.time - startTime;
            resultText.text = FormatTime(timeDiff);
            if (timeDiff < bestTime || bestTime == 0f)
            {
                bestTime = timeDiff;
                presenter.SetBestTime(bestTime);
                UpdateBestTimeText();
            }
            presenter.EndGame();
            currentState = GameStates.IDLE;
        }
        
        private void OnOpenSkinsShop()
        {
        }

        private IEnumerator StartLights()
        {
            for (int i = 0; i < lightStrips.Length; i++)
            {
                lightStrips[i].SetActive(true);
                yield return new WaitForSeconds(LIGHT_ON_INTERVAL);
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            ClearLights();
            startTime = Time.time;
            currentState = GameStates.WAITING;
        }

        private void UpdateBestTimeText()
        {
            bestTimeText.text = $"Best Time: {FormatTime(bestTime)}";
        }

        private string FormatTime(float time)
        {
            int seconds = (int)time;
            float milliseconds = (time - seconds) * 1000f;
            return $"{seconds:00}.{milliseconds:000}";
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