using System;
using UnityEngine;

namespace CodeBase.UI.Screens.Car
{
    public interface ICarPresenter
    {
        event Action<Sprite> ChangedSelectedCar;
        event Action<int> CoinsAmount;
        event Action<string> PlayCarAnimation;
        event Action ResetCarAnimation;
        Sprite SelectedCar { get; }
        void Subscribe();
        void Unsubscribe();
        void PlayAnimation(string type);
        void SetCoin(int coinsCount);
        void ResetAnimation();
    }
}