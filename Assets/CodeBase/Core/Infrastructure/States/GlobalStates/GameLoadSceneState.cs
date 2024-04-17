using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Services.Factories;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoadSceneState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IPersistentProgressService progressService;
        private readonly IGameFactory gameFactory;
        private readonly IUIFactory uiFactory;
        private readonly IHUDService hudService;

        public GameLoadSceneState(GameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            IPersistentProgressService progressService,
            IGameFactory gameFactory,
            IUIFactory uiFactory, 
            IHUDService hudService, 
            ILogService log)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingCurtain = loadingCurtain;
            this.uiFactory = uiFactory;
            this.hudService = hudService;
            this.log = log;
            this.progressService = progressService;
            this.gameFactory = gameFactory;
        }

        private readonly ILogService log;

        private BuildInfoConfig buildInfoConfig;

        public async void Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            await sceneLoader.Load(SceneNames.GameScene);
            InitGameWorld();
            gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            loadingCurtain.Hide();
            log.LogState("Exit", this);
        }

        private void InitGameWorld()
        {
            buildInfoConfig = new BuildInfoConfig();
            buildInfoConfig.BuildNumber = Application.version;
            
            uiFactory.CreateUIRoot();
            hudService.ShowSettingBar();
            hudService.ShowBuildInfo(buildInfoConfig);
            
            LoadProgressReader();
        }
        
        private void LoadProgressReader()
        {
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            log.LogState("Notify progress reader complete load data for object", this);
        }
    }
}