using System.Collections.Generic;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.Randomizer;
using CodeBase.Core.StaticData.Game;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class LeaderboardPresenter : ILeaderboardPresenter
    {
        private readonly IPersistentProgressService progressService;
        private readonly IRandomService randomService;
        private readonly IAssetProvider assetProvider;
        private readonly List<Sprite> randomSprites = new List<Sprite>();
        private int thisPlayerDataRank;
        
        public List<Sprite> RandomSprites => randomSprites;
        public Sprite SelectedCar => progressService.SelectedCar.CarSprite;
        public int ThisPlayerDataRank => thisPlayerDataRank;

        public LeaderboardPresenter(IPersistentProgressService progressService, IRandomService randomService, IAssetProvider assetProvider)
        {
            this.progressService = progressService;
            this.randomService = randomService;
            this.assetProvider = assetProvider;
        }

        public void Subscribe()
        {
            CreateRandomSprite(15);
            YandexGame.onGetLeaderboard += GetThisPlayerDataRank;
        }

        public void Unsubscribe()
        {
            YandexGame.onGetLeaderboard -= GetThisPlayerDataRank;
        }

        private void GetThisPlayerDataRank(LBData data)
        {
            thisPlayerDataRank = data.thisPlayer.rank - 1;
        }

        private void CreateRandomSprite(int count)
        {
            AllSpriteCarConfig config = assetProvider.Load<AllSpriteCarConfig>(InfrastructurePath.AllSpriteCarConfigPath);
            for (int i = 0; i < count; i++)
            {
                Sprite view = config.ImagesContainer[randomService.Next(0, config.ImagesContainer.Length)];
                randomSprites.Add(view);
            }
        }
    }
}