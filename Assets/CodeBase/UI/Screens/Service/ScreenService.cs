using System.Collections.Generic;
using System.Threading;
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
        private readonly CancellationTokenSource ctn;

        public ScreenService(IFrameSupplier<ScreenName, UnityFrame> supplier)
        {
            this.supplier = supplier;
        }

        public void ShowGameView()
        {
            if (supplier.LoadFrame(ScreenName.GAME) is not GameViewScreen gameView) return;
            
            IGamePresenter presenter = new GamePresenter(
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<ISaveService>());
            RegisterProgress(presenter);
            
            gameView.Show(presenter);
        }
        
        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
        
    }
}