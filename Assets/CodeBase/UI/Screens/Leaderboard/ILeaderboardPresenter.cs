using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Screens.Leaderboard
{
    public interface ILeaderboardPresenter 
    {
        List<Sprite> RandomSprites { get; }
        Sprite SelectedCar { get; }
        int ThisPlayerDataRank { get; }
        void CreateRandomSprite(int count);
        void Subscribe();
        void Unsubscribe();
    }
}