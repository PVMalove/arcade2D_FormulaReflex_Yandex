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
            AllSpriteCarConfig config = assetProvider.Load<AllSpriteCarConfig>(InfrastructurePath.AllSpriteCarConfigPath);
            for (int i = 0; i < count; i++)
            {
                Sprite view = config.ImagesContainer[randomService.Next(0, config.ImagesContainer.Length)];
                randomSprites.Add(view);
            }
        }
    }
}