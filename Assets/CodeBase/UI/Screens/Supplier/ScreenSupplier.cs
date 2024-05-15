using System;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Car;
using CodeBase.UI.Screens.Game;
using CodeBase.UI.Screens.Leaderboard;
using CodeBase.UI.Screens.Shop;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Screens.Supplier
{
    public class ScreenSupplier : FrameSupplier<ScreenName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public ScreenSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(ScreenName key)
        {
            switch (key)
            {
                case ScreenName.None:
                    break;
                case ScreenName.CAR:
                    CarViewScreen carView = uiFactory.CreateCarView();
                    carView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    carView.name = "CarView";
                    return carView;
                case ScreenName.IDLE:
                    IdleGameViewScreen idleView = uiFactory.CreateIdleGameView();
                    idleView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    idleView.name = "IdleView";
                    return idleView;
                case ScreenName.RUNNING:
                    RunningGameViewScreen runningView = uiFactory.CreateRunningGameView();
                    runningView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    runningView.name = "RunningView";
                    return runningView;
                case ScreenName.LOST:
                    LostGameViewScreen lostView = uiFactory.CreateLostGameView();
                    lostView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    lostView.name = "LostView";
                    return lostView;
                case ScreenName.ENDED:
                    EndedGameViewScreen endedView = uiFactory.CreateEndedGameView();
                    endedView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    endedView.name = "EndedView";
                    return endedView;
                case ScreenName.LEADERBOARD:
                    LeaderboardViewScreen leaderboardView = uiFactory.CreateLeaderboardView();
                    leaderboardView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    leaderboardView.name = "LeaderboardView";
                    return leaderboardView;
                case ScreenName.SHOP:
                    StoreViewScreen storeView = uiFactory.CreateStoreView();
                    storeView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    storeView.name = "StoreView";
                    return storeView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}