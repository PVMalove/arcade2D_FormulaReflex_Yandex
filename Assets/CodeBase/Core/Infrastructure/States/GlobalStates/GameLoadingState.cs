using System.Threading.Tasks;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.StaticData.Infrastructure;
using UnityEngine;


namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoadingState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IPersistentProgressService progressService;
        private readonly ILoadService loadService;
        private readonly IAssetProvider assetProvider;
        private readonly ILogService log;


        public GameLoadingState(GameStateMachine gameStateMachine,
            ILoadingCurtain loadingCurtain,
            IPersistentProgressService progressService,
            ILoadService loadService,
            IAssetProvider assetProvider,
            ILogService log)
        {
            this.gameStateMachine = gameStateMachine;
            this.loadingCurtain = loadingCurtain;
            this.progressService = progressService;
            this.loadService = loadService;
            this.assetProvider = assetProvider;
            this.log = log;
        }

        public async void Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            await CompleteLoadData();
            gameStateMachine.Enter<GameLoadSceneState>();
        }

        public void Exit()
        {
            loadingCurtain.Hide();
            log.LogState("Exit", this);
        }

        private async Task CompleteLoadData()
        {
            PlayerProgress progress = await loadService.LoadProgress();

            progressService.Initialize(progress ?? await NewProgress());
            log.LogState($"CompleteLoadData player progress: {progressService.GetProgress().ToJson()}", this);
        }

        private async Task<PlayerProgress> NewProgress()
        {
            FirstSaveData newSaveData = await assetProvider.LoadAsync<FirstSaveData>(InfrastructurePath.NewSaveDataPath);

            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume,
                newSaveData.MusicOn,
                newSaveData.EffectsOn
            );

            CoinData coinData = new CoinData(
                newSaveData.CoinsAmount
            );


            PlayerProgress progress = new PlayerProgress(
                audioControl,
                coinData);

            log.LogState("Init new player progress", this);
            return progress;
        }
    }
}