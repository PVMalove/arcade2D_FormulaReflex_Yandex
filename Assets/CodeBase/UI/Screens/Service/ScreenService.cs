using System.Collections.Generic;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.PoolService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.Randomizer;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Car;
using CodeBase.UI.Screens.Game;
using CodeBase.UI.Screens.Leaderboard;
using CodeBase.UI.Screens.Shop;
using CodeBase.UI.Screens.Shop.Item;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Screens.Service
{
    public class ScreenService : IScreenService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();
        
        private readonly IFrameSupplier<ScreenName, UnityFrame> supplier;
        
        private ICarPresenter carPresenter;
        private IGamePresenter gamePresenter;
        private ILeaderboardPresenter leaderboardPresenter;
        private IStorePresenter storePresenter;

        public ScreenService(IFrameSupplier<ScreenName, UnityFrame> supplier)
        {
            this.supplier = supplier; 
        }

        public void InitializePresenter()
        {
            
            carPresenter = new CarPresenter(
                AllServices.Container.Single<IPersistentProgressService>());
            
            gamePresenter = new GamePresenter(
                AllServices.Container.Single<IScreenService>(),
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<ISaveService>());
            RegisterProgress(gamePresenter);

            leaderboardPresenter = new LeaderboardPresenter(
                AllServices.Container.Single<IRandomService>(),
                AllServices.Container.Single<IAssetProvider>());

            storePresenter = new StorePresenter(
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<IStaticDataService>()
            );
        }

        public void ShowCarView()
        {
            if (supplier.LoadFrame(ScreenName.CAR) is not CarViewScreen view) return;
            view.Show(carPresenter);
        }

        public void ShowIdleGameView()
        {
            if (supplier.LoadFrame(ScreenName.IDLE) is not IdleGameViewScreen view) return;
            view.Show(gamePresenter);
        }

        public void ShowRunningGameView()
        {
            if (supplier.LoadFrame(ScreenName.RUNNING) is not RunningGameViewScreen view) return;
            view.Show(gamePresenter);
        }

        public void ShowLostGameView()
        {
            if (supplier.LoadFrame(ScreenName.LOST) is not LostGameViewScreen view) return;
            view.Show(gamePresenter);
        }

        public void ShowEndedGameView()
        {
            if (supplier.LoadFrame(ScreenName.ENDED) is not EndedGameViewScreen view) return;
            view.Show(gamePresenter);
        }

        public void ShowLeaderboardView()
        {
            if (supplier.LoadFrame(ScreenName.LEADERBOARD) is not LeaderboardViewScreen view) return;
            view.Show(leaderboardPresenter);
        }

        public void ShowShopView()
        {
            if (supplier.LoadFrame(ScreenName.SHOP) is not StoreViewScreen view) return;
            view.GetComponent<ShopItemsPresenter>().Construct(
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IPoolFactory>());
            view.GetComponent<ShopItemsPresenter>().Initialize();
            view.Show(storePresenter);
        }

        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}