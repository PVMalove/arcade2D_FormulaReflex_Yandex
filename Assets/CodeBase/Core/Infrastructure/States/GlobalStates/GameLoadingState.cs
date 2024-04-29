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
        private readonly GlobalStateMachine globalStateMachine;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IPersistentProgressService progressService;
        private readonly ILoadService loadService;
        private readonly IAssetProvider assetProvider;
        private readonly ILogService log;


        public GameLoadingState(GlobalStateMachine globalStateMachine,
            ILoadingCurtain loadingCurtain,
            IPersistentProgressService progressService,
            ILoadService loadService,
            IAssetProvider assetProvider,
            ILogService log)
        {
            this.globalStateMachine = globalStateMachine;
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
            globalStateMachine.Enter<GameLoadSceneState>();
        }

        public void Exit()
        {
            loadingCurtain.Hide();
            log.LogState("Exit", this);
        }

        private async Task CompleteLoadData()
        {
            PlayerProgress progress = await loadService.LoadProgress();

            progressService.Initialize(progress ?? NewProgress());
            log.LogState($"CompleteLoadData player progress: {progressService.GetProgress().ToJson()}", this);
        }

        private PlayerProgress NewProgress()
        {
            FirstSaveData newSaveData = assetProvider.Load<FirstSaveData>(InfrastructurePath.NewSaveDataPath);

            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume,
                newSaveData.EffectsOn
            );

            CoinData coinData = new CoinData(
                newSaveData.CoinsAmount
            );
            
            BestTimeData bestTimeData = new BestTimeData(
                newSaveData.BestTime
            );

            PlayerProgress progress = new PlayerProgress(
                audioControl,
                coinData,
                bestTimeData);

            log.LogState("Init new player progress", this);
            return progress;
        }
    }
}