using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using CodeBase.UI.Screens.Car;
using CodeBase.UI.Screens.Game;
using CodeBase.UI.Screens.Leaderboard;
using CodeBase.UI.Screens.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        public IUIRoot UIRoot { get; private set; }

        public UIFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public void CreateUIRoot()
        {
            GameObject uiRoot = assetProvider.Instantiate(InfrastructurePath.UIRootPath);
            uiRoot.name = "GameUICanvas";
            UIRoot = uiRoot.GetComponent<IUIRoot>();
        }

        public BuildInfoViewHUD CreateBuildInfoView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.BuildInfoViewPath);
            return view.GetComponent<BuildInfoViewHUD>();
        }

        public SettingBarViewHUD CreateSettingBarView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.SettingBarViewPath);
            return view.GetComponent<SettingBarViewHUD>();
        }

        public CarViewScreen CreateCarView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.CarViewScreen);
            return view.GetComponent<CarViewScreen>();
        }
        public IdleGameViewScreen CreateIdleGameView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.IdleGameViewScreen);
            return view.GetComponent<IdleGameViewScreen>();
        }

        public RunningGameViewScreen CreateRunningGameView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.RunningGameViewScreen);
            return view.GetComponent<RunningGameViewScreen>();
        }

        public LostGameViewScreen CreateLostGameView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.LostGameViewScreen);
            return view.GetComponent<LostGameViewScreen>();
        }
        
        public EndedGameViewScreen CreateEndedGameView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.EndedGameViewScreen);
            return view.GetComponent<EndedGameViewScreen>();
        }

        public LeaderboardViewScreen CreateLeaderboardView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.LeaderboardViewScreen);
            return view.GetComponent<LeaderboardViewScreen>();
        }

        public StoreViewScreen CreateStoreView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.StoreViewScreen);
            return view.GetComponent<StoreViewScreen>();
        }
    }
}