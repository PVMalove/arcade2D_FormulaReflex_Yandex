using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.HUD.Supplier;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly IAudioService audioService;
        private readonly AllServices services;


        public GameBootstrapState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IAudioService audioService, AllServices services)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.services = services;
            this.audioService = audioService;
            RegisterServices();
        }

        public async void Enter()
        {
            Debug.Log("[StateMachine] - GameBootstrapState: Enter");
            await sceneLoader.Load(SceneNames.BoostrapScene);
            gameStateMachine.Enter<GameLoadingState>();
        }

        public void Exit()
        {
            Debug.Log("[StateMachine] - GameBootstrapState: Exit");
        }

        private void RegisterServices()
        {
            services.RegisterSingle<IStateMachine>(gameStateMachine);
            
            services.RegisterSingle<ILogService>(new LogService());
            
            services.RegisterSingle<IAudioService>(audioService);
            
            services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            services.RegisterSingle<IGameFactory>(new GameFactory(
                services.Single<IPersistentProgressService>()));
            
            services.RegisterSingle<IUIFactory>(new UIFactory(
                services.Single<IAssetProvider>()));
            
            services.RegisterSingle<IFrameSupplier<HUDName, UnityFrame>>(new HUDSupplier(
                services.Single<IUIFactory>()));
            
            services.RegisterSingle<IHUDService>(new HUDService( 
                services.Single<IFrameSupplier<HUDName, UnityFrame>>()));

            services.RegisterSingle<ISaveService>(new SaveService(
                services.Single<IGameFactory>(),
                services.Single<IHUDService>(),
                services.Single<IPersistentProgressService>(),
                services.Single<ILogService>()));
            
            services.RegisterSingle<ILoadService>(new LoadService(
                services.Single<ILogService>()));
        }
    }
}