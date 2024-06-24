using System;
using CodeBase.Core.Services.ProgressService;
using UnityEngine;

namespace CodeBase.UI.Screens.Car
{
    public class CarPresenter : ICarPresenter
    {
        public event Action<Sprite> ChangedSelectedCar;
        public event Action<int> CoinsAmount;
        public event Action<String> PlayCarAnimation;
        public event Action ResetCarAnimation;
        
        public Sprite SelectedCar => progressService.SelectedCar.CarSprite;

        private readonly IPersistentProgressService progressService;
        public CarPresenter(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }
        
        public void Subscribe()
        {
            progressService.SelectedCarChanged += OnSelectedCarChanged;
            progressService.CoinsAmountChanged += OnCoinsAmountChanged;
        }

        public void Unsubscribe()
        {
            progressService.SelectedCarChanged -= OnSelectedCarChanged;
            progressService.CoinsAmountChanged -= OnCoinsAmountChanged;
        }

        private void OnCoinsAmountChanged() => 
            CoinsAmount?.Invoke(progressService.CoinsAmount);

        public void PlayAnimation(string type) => 
            PlayCarAnimation?.Invoke(type);
        
        public void ResetAnimation() => 
            ResetCarAnimation?.Invoke();

        private void OnSelectedCarChanged(Sprite view) => 
            ChangedSelectedCar?.Invoke(view);
    }
}