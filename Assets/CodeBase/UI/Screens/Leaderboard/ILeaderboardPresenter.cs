using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Screens.Leaderboard
{
    public interface ILeaderboardPresenter 
    {
        List<Sprite> RandomSprites { get; }
        Sprite SelectedCar { get; }
        int ThisPlayerDataRank { get; }
        void Subscribe();
        void Unsubscribe();
        void RestartGame();
    }
}