using System;
using System.Collections.Generic;
using CodeBase.Core.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Core.StaticData.UI.Shop;
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
        private readonly IAudioService audioService;
        private readonly IStaticDataService staticDataService;
        private readonly ICarPresenter carPresenter;

        private Dictionary<CarType, CarStoreItemConfig> skinsData;
        
        private float bestTime;
        private float startTime;
        private float timeDiff;
        private int coinsAmount;
        
        public string TimeDiff => FormatTime(timeDiff);
        public string BestTime => FormatTime(bestTime);
        public string CoinsAmount => coinsAmount.ToString();
        public IAudioService AudioService => audioService;
        public Dictionary<CarType, CarStoreItemConfig> SkinsData => skinsData;

        public GamePresenter(IScreenService screenService,
            IPersistentProgressService progressService,
            ISaveService saveService,
            IAudioService audioService,
            IStaticDataService staticDataService,
            ICarPresenter carPresenter)
        {
            this.screenService = screenService;
            this.progressService = progressService;
            this.saveService = saveService;
            this.audioService = audioService;
            this.staticDataService = staticDataService;
            this.carPresenter = carPresenter;
        }
        
        public void Subscribe()
        {
            skinsData = staticDataService.StoreItemsCatalog.CarItems;
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
            audioService.FX_f_motor_SourceAudio.Play("f1_01");
        }

        public void StopGame()
        {
            audioService.FX_f_motor_SourceAudio.Stop();
            audioService.FX_f_start_SourceAudio.Play("f1_03");
            carPresenter.PlayAnimation("LostGame");
            screenService.ShowLostGameView();
        }

        public void RestartGame()
        {
            carPresenter.ResetAnimation();
            saveService.SaveProgress();
            screenService.ShowIdleGameView();
        }

        public void EndGame()
        {
            timeDiff = Time.time - startTime;
            int coinsCount = (int)(10 / timeDiff);
            
            if (timeDiff < bestTime || bestTime == 0f)
            {
                coinsCount = (int)(100 / timeDiff);
                bestTime = timeDiff;
                YandexGame.NewLBScoreTimeConvert("BestTimeRecord2", bestTime);
            }
            
            progressService.AddCoins(coinsCount);
            saveService.SaveProgress();
            carPresenter.SetCoin(coinsCount);
            audioService.FX_f_motor_SourceAudio.Stop();
            audioService.FX_f_start_SourceAudio.Play("f1_02");
            carPresenter.PlayAnimation("EndGame");
            screenService.ShowEndedGameView();
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