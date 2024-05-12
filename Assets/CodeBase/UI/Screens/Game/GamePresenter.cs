using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.UI.Screens.Service;
using UnityEngine;
using YG;

namespace CodeBase.UI.Screens.Game
{
    public sealed class GamePresenter : IGamePresenter
    {
        private float bestTime;
        private float startTime;
        private float timeDiff;
        public string TimeDiff => FormatTime(timeDiff);
        public string BestTime => FormatTime(bestTime);
        public int CoinsAmount { get; private set; }

        private readonly IScreenService screenService;
        private readonly IPersistentProgressService progressService;
        private readonly ISaveService saveService;

        public GamePresenter(IScreenService screenService,
            IPersistentProgressService progressService, ISaveService saveService)
        {
            this.screenService = screenService;
            this.progressService = progressService;
            this.saveService = saveService;
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
            screenService.ShowEndedGameView();
        }
        
        public void OpenLeaderboard()
        {
            screenService.ShowLeaderboardView();
        }


        public void SetStartTime(float time)
        {
            startTime = time;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            bestTime = progress.BestTimeData.Value;
            CoinsAmount = progressService.GetProgress().CoinData.CoinsAmount;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CoinData.CoinsAmount = CoinsAmount;
            progress.BestTimeData.Value = bestTime;
        }

        private string FormatTime(float time)
        {
            int seconds = (int)time;
            float milliseconds = (time - seconds) * 1000f;
            return $"{seconds:00}.{milliseconds:000}";
        }
    }
}