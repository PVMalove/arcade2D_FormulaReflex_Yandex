using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;

namespace CodeBase.UI.Screens.Game
{
    public interface IGamePresenter : IProgressSaver
    {
        string BestTime { get; }
        int CoinsAmount { get; }
        string TimeDiff { get; }
        void StartGame();
        void StopGame();
        void RestartGame();
        void SetBestTime(float time);
        void SetStartTime(float time);
        void EndGame();
    }
}