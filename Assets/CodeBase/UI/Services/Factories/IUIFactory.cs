using CodeBase.Core.Infrastructure.SceneManagement.Services;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;

namespace CodeBase.UI.Services.Factories
{
    public interface IUIFactory : IService
    {
        IUIRoot UIRoot { get; }
        void CreateUIRoot();
        BuildInfoViewHUD CreateBuildInfoView();
        SettingBarViewHUD CreateSettingBarView();
        void Cleanup();
    }
}