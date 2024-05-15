using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.UI.Screens.Service
{
    public interface IScreenService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        void InitializePresenter();
        
        void ShowCarView();
        void ShowIdleGameView();
        void ShowRunningGameView();
        void ShowLostGameView();
        void ShowEndedGameView();
        void ShowLeaderboardView();
        void ShowShopView();
    }
}