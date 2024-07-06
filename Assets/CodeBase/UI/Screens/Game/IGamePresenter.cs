using System;
using System.Collections.Generic;
using CodeBase.Core.Audio.Service;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;

namespace CodeBase.UI.Screens.Game
{
    public interface IGamePresenter : IProgressSaver
    {
        event Action ChangedCoinsAmount;
        event Action<Sprite> ChangedSelectedCar; 
        
        string BestTime { get; }
        string CoinsAmount { get; }
        string TimeDiff { get; }
        IAudioService AudioService { get; }
        Dictionary<CarType, CarStoreItemConfig> SkinsData { get; }

        void Subscribe();
        void Unsubscribe();
        void StartGame();
        void StopGame();
        void RestartGame();
        void EndGame();
        void SetStartTime(float time);
        void OpenLeaderboard();
        void OpenShop();
    }
}