using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Game;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Screens.Service
{
    public class ScreenService : IScreenService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();
        
        private readonly IFrameSupplier<ScreenName, UnityFrame> supplier;
        private IGamePresenter presenter;

        public ScreenService(IFrameSupplier<ScreenName, UnityFrame> supplier)
        {
            this.supplier = supplier; 
        }
        
        public void ShowIdleGameView()
        {
            if (supplier.LoadFrame(ScreenName.IDLE) is not IdleGameViewScreen view) return;
            view.Show(presenter);
        }
        
        public void ShowRunningGameView()
        {
            if (supplier.LoadFrame(ScreenName.RUNNING) is not RunningGameViewScreen view) return;
            view.Show(presenter);
        }

        public void ShowLostGameView()
        {
            if (supplier.LoadFrame(ScreenName.LOST) is not LostGameViewScreen view) return;
            view.Show(presenter);
        }

        public void ShowEndedGameView()
        {
            if (supplier.LoadFrame(ScreenName.ENDED) is not EndedGameViewScreen view) return;
            view.Show(presenter);
        }


        public void CreateGamePresenter()
        {
            presenter = new GamePresenter(
                AllServices.Container.Single<IScreenService>(),
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<ISaveService>());
            RegisterProgress(presenter);
        }

        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
        
    }
}