using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Popup.Base;
using CodeBase.UI.Popup.Service;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Service;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public class GlobalStateMachine : StateMachine
    {
        public GlobalStateMachine(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, IAudioService audioService,
            AllServices services)
        {
            RegisterState(new GameBootstrapState(this, sceneLoader,audioService, services));
            RegisterState(new GameLoadingState(this, loadingCurtain,
                services.Single<IStaticDataService>(),
                services.Single<IPersistentProgressService>(),
                services.Single<ILoadService>(),
                services.Single<IAssetProvider>(),
                services.Single<ILogService>()));
            RegisterState(new GameLoadSceneState(this, sceneLoader,loadingCurtain,
                services.Single<IPersistentProgressService>(),
                services.Single<IGameFactory>(),
                services.Single<IUIFactory>(),
                services.Single<IHUDService>(),
                services.Single<IScreenService>(),
                services.Single<IPopupService>(),
                services.Single<ILogService>()));
            RegisterState(new GameLoopState(
                services.Single<IFrameSupplier<HUDName, UnityFrame>>(),
                services.Single<IFrameSupplier<ScreenName, UnityFrame>>(),
                services.Single<IFrameSupplier<PopupName, UnityFrame>>(),
                services.Single<ILogService>()));
        }
        
        public void Start() => Enter<GameBootstrapState>();
    }
}