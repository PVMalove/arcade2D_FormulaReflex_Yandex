using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Screens.Service;
using CodeBase.UI.Services.Factories;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoadSceneState : IState
    {
        private readonly GlobalStateMachine globalStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IPersistentProgressService progressService;
        private readonly IGameFactory gameFactory;
        private readonly IUIFactory uiFactory;
        private readonly IHUDService hudService;
        private readonly IScreenService screenService;
        private readonly ILogService log;
        
        private BuildInfoConfig buildInfoConfig;

        public GameLoadSceneState(GlobalStateMachine globalStateMachine, 
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            IPersistentProgressService progressService,
            IGameFactory gameFactory,
            IUIFactory uiFactory, 
            IHUDService hudService, 
            IScreenService screenService,
            ILogService log)
        {
            this.globalStateMachine = globalStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingCurtain = loadingCurtain;
            this.progressService = progressService;
            this.gameFactory = gameFactory;
            this.uiFactory = uiFactory;
            this.hudService = hudService;
            this.screenService = screenService;
            this.log = log;
        }

        public async void Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            await sceneLoader.Load(SceneNames.GameScene);
            InitGameWorld();
            globalStateMachine.Enter<GameLoopState>();
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
            
            screenService.InitializePresenter();
            LoadProgressReader();
            
            screenService.ShowIdleGameView();
        }
        
        private void LoadProgressReader()
        {
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in screenService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            
            log.LogState($"Notify progress reader complete load data for object - {progressService.GetProgress().BestTimeData.Value}", this);
        }
    }
}