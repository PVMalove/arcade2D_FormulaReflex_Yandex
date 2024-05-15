using CodeBase.Core.Services.ServiceLocator;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using CodeBase.UI.Screens.Car;
using CodeBase.UI.Screens.Game;
using CodeBase.UI.Screens.Leaderboard;
using CodeBase.UI.Screens.Shop;

namespace CodeBase.UI.Services.Factories
{
    public interface IUIFactory : IService
    {
        IUIRoot UIRoot { get; }
        void CreateUIRoot();
        BuildInfoViewHUD CreateBuildInfoView();
        SettingBarViewHUD CreateSettingBarView();
        CarViewScreen CreateCarView();
        IdleGameViewScreen CreateIdleGameView();
        RunningGameViewScreen CreateRunningGameView();
        LostGameViewScreen CreateLostGameView();
        EndedGameViewScreen CreateEndedGameView();
        LeaderboardViewScreen CreateLeaderboardView();
        StoreViewScreen CreateStoreView();
    }
}