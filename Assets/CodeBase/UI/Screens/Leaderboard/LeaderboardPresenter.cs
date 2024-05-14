using System.Collections.Generic;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.Randomizer;
using CodeBase.Core.StaticData.Game;
using UnityEngine;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class LeaderboardPresenter : ILeaderboardPresenter
    {
        private readonly IRandomService randomService;
        private readonly IAssetProvider assetProvider;
        private readonly List<Sprite> randomSprites = new List<Sprite>();

        public List<Sprite> RandomSprites => randomSprites;

        public LeaderboardPresenter(IRandomService randomService, IAssetProvider assetProvider)
        {
            this.randomService = randomService;
            this.assetProvider = assetProvider;
            
            CreateRandomSprite(15);
        }

        public void CreateRandomSprite(int count)
        {
            AllCarViewConfig allCarViewConfig = assetProvider.Load<AllCarViewConfig>(InfrastructurePath.AllCarViewConfigPath);
            for (int i = 0; i < count; i++)
            {
                Sprite car = allCarViewConfig.ImagesContainer[randomService.Next(0, allCarViewConfig.ImagesContainer.Length)];
                randomSprites.Add(car);
            }
        }
    }
}