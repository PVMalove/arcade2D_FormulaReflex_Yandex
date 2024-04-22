using System.Collections.Generic;
using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Screens.Service;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.HUD.Service
{
    public class HUDService : IHUDService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        private readonly IFrameSupplier<HUDName, UnityFrame> supplier;


        public HUDService(IFrameSupplier<HUDName, UnityFrame> supplier)
        {
            this.supplier = supplier;
        }

        public void ShowBuildInfo(BuildInfoConfig config)
        {
            if (supplier.LoadFrame(HUDName.BUILD_INFO) is not BuildInfoViewHUD buildInfoView) return;
            
            IBuildInfoPresenter presenter = new BuildInfoPresenter(config);
            buildInfoView.Show(presenter);
        }
        
        public void ShowSettingBar()
        {
            if (supplier.LoadFrame(HUDName.SETTING_BAR) is not SettingBarViewHUD settingBarView) return;
            
            ISettingBarPresenter presenter = new SettingBarPresenter(
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<IAssetProvider>(),
                AllServices.Container.Single<IGameFactory>(),
                AllServices.Container.Single<IHUDService>(),
                AllServices.Container.Single<IScreenService>(),
                AllServices.Container.Single<IAudioService>());
            
            RegisterProgress(presenter);
            settingBarView.Show(presenter);
        }


        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}