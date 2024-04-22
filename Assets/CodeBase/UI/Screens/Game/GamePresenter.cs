using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;

namespace CodeBase.UI.Screens.Game
{
    public sealed class GamePresenter : IGamePresenter
    {
        public int CoinsAmount { get; private set; }
        public float BestTime { get; private set; }

        public PlayerProgress Progress => progressService.GetProgress();
        
        
        private readonly IPersistentProgressService progressService;
        private readonly ISaveService saveService;

        public GamePresenter(IPersistentProgressService progressService, ISaveService saveService)
        {
            this.progressService = progressService;
            this.saveService = saveService;
        }

        public void StartGame()
        {
            

        }

        public void StopGame()
        {

        }

        public void EndGame()
        {
            saveService.SaveProgress();
        }
        
        public void SetBestTime(float time)
        {
            BestTime = time;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            CoinsAmount = progressService.GetProgress().CoinData.CoinsAmount;
            BestTime = progressService.GetProgress().BestTimeData.Value;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progressService.GetProgress().CoinData.CoinsAmount = CoinsAmount;
            progressService.GetProgress().BestTimeData.Value = BestTime;
        }
    }
}