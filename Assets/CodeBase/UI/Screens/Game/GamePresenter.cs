using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.UI.Screens.Car;
using CodeBase.UI.Screens.Service;
using UnityEngine;
using YG;

namespace CodeBase.UI.Screens.Game
{
    public sealed class GamePresenter : IGamePresenter
    {
        public event Action ChangedCoinsAmount;
        public event Action<Sprite> ChangedSelectedCar;

        private readonly IScreenService screenService;
        private readonly IPersistentProgressService progressService;
        private readonly ISaveService saveService;
        private readonly ICarPresenter carPresenter;

        private float bestTime;
        private float startTime;
        private float timeDiff;
        private int coinsAmount;
        
        public string TimeDiff => FormatTime(timeDiff);
        public string BestTime => FormatTime(bestTime);
        public string CoinsAmount => coinsAmount.ToString();

        public GamePresenter(IScreenService screenService,
            IPersistentProgressService progressService, ISaveService saveService,
            ICarPresenter carPresenter)
        {
            this.screenService = screenService;
            this.progressService = progressService;
            this.saveService = saveService;
            this.carPresenter = carPresenter;
        }
        
        public void Subscribe()
        {
            progressService.CoinsAmountChanged += OnCoinsAmountChanged;
            progressService.SelectedCarChanged += OnSelectedCarChanged;
        }

        public void Unsubscribe()
        {
            progressService.CoinsAmountChanged -= OnCoinsAmountChanged;
            progressService.SelectedCarChanged -= OnSelectedCarChanged;
        }

        public void StartGame()
        {
            screenService.ShowRunningGameView();
        }

        public void StopGame()
        {
            screenService.ShowLostGameView();
        }

        public void RestartGame()
        {
            saveService.SaveProgress();
            carPresenter.ResetAnimation();
            screenService.ShowIdleGameView();
        }

        public void EndGame()
        {
            timeDiff = Time.time - startTime;
            
            if (timeDiff < bestTime || bestTime == 0f)
            {
                bestTime = timeDiff;
                YandexGame.NewLBScoreTimeConvert("BestTimeRecord2", bestTime);
            }
            saveService.SaveProgress();
            carPresenter.PlayAnimation();
            screenService.ShowEndedGameView();
            //carPresenter.ResetAnimation();
        }

        public void OpenLeaderboard()
        {
            screenService.ShowLeaderboardView();
        }

        public void OpenShop()
        {
            screenService.ShowShopView();
        }

        public void SetStartTime(float time)
        {
            startTime = time;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            bestTime = progress.BestTimeData.Value;
            coinsAmount = progress.CoinData.CoinsAmount;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CoinData.CoinsAmount = coinsAmount;
            progress.BestTimeData.Value = bestTime;
        }

        private void OnCoinsAmountChanged()
        {
            coinsAmount = progressService.CoinsAmount;
            ChangedCoinsAmount?.Invoke();
        }

        private void OnSelectedCarChanged(Sprite view) => 
            ChangedSelectedCar?.Invoke(view);

        private string FormatTime(float time)
        {
            int seconds = (int)time;
            float milliseconds = (time - seconds) * 1000f;
            return $"{seconds:00}.{milliseconds:000}";
        }
    }
}