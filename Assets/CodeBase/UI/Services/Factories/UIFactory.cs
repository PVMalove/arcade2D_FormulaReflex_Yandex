using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using CodeBase.UI.Windows.GameCanvas;
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

        public GameViewScreen CreateGameView()
        {
            GameObject view = assetProvider.Instantiate(InfrastructurePath.GameMenuViewScreen);
            return view.GetComponent<GameViewScreen>();
        }

        public void Cleanup()
        {
        }
    }
}