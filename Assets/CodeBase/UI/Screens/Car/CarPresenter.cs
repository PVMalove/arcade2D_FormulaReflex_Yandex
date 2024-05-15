using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using UnityEngine;

namespace CodeBase.UI.Screens.Car
{
    public class CarPresenter : ICarPresenter
    {
        public event Action<Sprite> ChangedSelectedCar;
        public Sprite SelectedCar => progressService.SelectedCar.CarSprite;

        private readonly IPersistentProgressService progressService;
        public CarPresenter(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }
        
        public void Subscribe()
        {
            progressService.SelectedCarChanged += OnSelectedCarChanged;
        }

        public void Unsubscribe()
        {
            progressService.SelectedCarChanged -= OnSelectedCarChanged;
        }

        private void OnSelectedCarChanged(Sprite view) => 
            ChangedSelectedCar?.Invoke(view);
    }
}