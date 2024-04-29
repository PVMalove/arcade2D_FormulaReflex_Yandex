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
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Screens.Service;
using CodeBase.UI.Services.Factories;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public class GlobalStateMachine : StateMachine
    {
        public GlobalStateMachine(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, IAudioService audioService,
            AllServices services)
        {
            RegisterState(new GameBootstrapState(this, sceneLoader,audioService, services));
            RegisterState(new GameLoadingState(this, loadingCurtain,
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
                services.Single<ILogService>()));
            RegisterState(new GameLoopState(services.Single<ILogService>()));
        }
        
        public void Start() => Enter<GameBootstrapState>();
    }
}