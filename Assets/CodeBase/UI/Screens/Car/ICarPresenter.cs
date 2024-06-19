using System;
using UnityEngine;

namespace CodeBase.UI.Screens.Car
{
    public interface ICarPresenter
    {
        event Action<Sprite> ChangedSelectedCar;
        event Action PlayAnimationEndGame;
        event Action ResetAnimationEndGame;
        Sprite SelectedCar { get; }
        void Subscribe();
        void Unsubscribe();
        void PlayAnimation();
        void ResetAnimation();
    }
}