using System;
using CodeBase.Core.Services.ProgressService;
using UnityEngine;

namespace CodeBase.UI.Screens.Car
{
    public interface ICarPresenter
    {
        event Action<Sprite> ChangedSelectedCar;
        Sprite SelectedCar { get; }
        void Subscribe();
        void Unsubscribe();
    }
}