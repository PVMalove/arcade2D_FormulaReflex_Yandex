using System.Collections.Generic;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.Randomizer;
using CodeBase.Core.Services.RestartGameService;
using CodeBase.Core.StaticData.Game;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class LeaderboardPresenter : ILeaderboardPresenter
    {
        private readonly IRestartGameService restartGameService;
        private readonly IPersistentProgressService progressService;
        private readonly IRandomService randomService;
        private readonly IAssetProvider assetProvider;
        private readonly List<Sprite> randomSprites = new List<Sprite>();

        private AllSpriteCarConfig config;
        private int thisPlayerDataRank;

        public List<Sprite> RandomSprites => randomSprites;
        public Sprite SelectedCar => progressService.SelectedCar.CarSprite;
        public int ThisPlayerDataRank => thisPlayerDataRank;

        public LeaderboardPresenter(IRestartGameService restartGameService,
            IPersistentProgressService progressService,
            IRandomService randomService, 
            IAssetProvider assetProvider)
        {
            this.restartGameService = restartGameService;
            this.progressService = progressService;
            this.randomService = randomService;
            this.assetProvider = assetProvider;
            Initialize();
        }

        private void Initialize()
        {
            config = assetProvider.Load<AllSpriteCarConfig>(InfrastructurePath.AllSpriteCarConfigPath);
            CreateRandomSprite(15);
            YandexGame.onGetLeaderboard += GetThisPlayerDataRank;
        }

        public void Subscribe()
        {
            YandexGame.onGetLeaderboard += GetThisPlayerDataRank;
        }

        public void Unsubscribe()
        {
            YandexGame.onGetLeaderboard -= GetThisPlayerDataRank;
        }
        
        public void RestartGame()
        {
            restartGameService.RestartGame();
        }
        
        private void GetThisPlayerDataRank(LBData data)
        {
            thisPlayerDataRank = data.thisPlayer.rank - 1;
        }

        private void CreateRandomSprite(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Sprite view = config.ImagesContainer[randomService.Next(0, config.ImagesContainer.Length)];
                randomSprites.Add(view);
            }
        }
    }
}