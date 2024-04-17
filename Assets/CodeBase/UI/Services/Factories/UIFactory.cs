using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
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

        public void Cleanup()
        {
        }
    }
}