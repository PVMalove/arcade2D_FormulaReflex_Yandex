using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Screens.Leaderboard
{
    public interface ILeaderboardPresenter 
    {
        List<Sprite> RandomSprites { get; }
        void CreateRandomSprite(int count);
    }
}