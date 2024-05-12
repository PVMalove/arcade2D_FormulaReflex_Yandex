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
            
            CreateRandomSprite(12);
        }

        public void CreateRandomSprite(int count)
        {
            BolideData bolideData = assetProvider.Load<BolideData>(InfrastructurePath.BolideDataPath);
            for (int i = 0; i < count; i++)
            {
                Sprite bolideSprite = bolideData.ImagesContainer[randomService.Next(0, bolideData.ImagesContainer.Length)];
                randomSprites.Add(bolideSprite);
            }
        }
    }
}