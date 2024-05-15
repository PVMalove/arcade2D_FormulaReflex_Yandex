using System;
using CodeBase.Core.Services.ProgressService;
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