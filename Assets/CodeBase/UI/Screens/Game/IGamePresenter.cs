using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;

namespace CodeBase.UI.Screens.Game
{
    public interface IGamePresenter : IProgressSaver
    {
        int CoinsAmount { get; }
        float BestTime { get; }
        PlayerProgress Progress { get; }
        void StartGame();
        void StopGame();
        void EndGame();
        void SetBestTime(float time);
    }
}