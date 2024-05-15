using System;
using System.Linq;
using System.Collections.Generic;
using CodeBase.Core.Data;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;

namespace CodeBase.Core.Services.ProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public event Action CoinsAmountChanged;
        public event Action<Sprite> SelectedCarChanged;

        private readonly IStaticDataService staticDataService;
        
        private PlayerProgress playerProgress;
        private List<CarStoreItemConfig> ownedCar;

        public int CoinsAmount => playerProgress.CoinData.CoinsAmount;
        public CarStoreItemConfig SelectedCar { get; private set; }
        public PlayerProgress GetProgress() => playerProgress;

        public PersistentProgressService(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public void Initialize(PlayerProgress progress)
        {
            playerProgress = progress;

            if (progress.PlayerCarData.OwnedItemsCar is null) 
                progress.PlayerCarData.OwnedItemsCar = new List<CarType> { CarType.f1_car_0 };
            
            ownedCar = progress.PlayerCarData.OwnedItemsCar
                .Select(GetValueCarItem)
                .ToList();
            
            SelectedCar = ownedCar.Find(x => x.Type == progress.PlayerCarData.SelectedCarType);
        }
        
        public void AddCoins(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Incorrect coins amount transferred!");
                return;
            }
            
            playerProgress.CoinData.CoinsAmount += amount;
            CoinsAmountChanged?.Invoke();
        }
        
        public void RemoveCoins(int amount)
        {
            if (!IsCoinsEnoughFor(amount))
            {
                Debug.LogError("Incorrect coins amount transferred!");
            }

            playerProgress.CoinData.CoinsAmount -= amount;
            CoinsAmountChanged?.Invoke();
        }

        public void OpenCarItem(CarType type)
        {
            if (IsPlayerOwnCar(type))
            {
                Debug.LogError($"Player already have such car type - {type}");
                return;
            }

            ownedCar.Add(GetValueCarItem(type));
            playerProgress.PlayerCarData.OwnedItemsCar.Add(type);
        }
        
        public void SelectedCarItem(CarType type)
        {
            if (!IsPlayerOwnCar(type))
            {
                Debug.LogError($"Player already have such car type - {type}");
                return;
            }
            
            SelectedCar = GetValueCarItem(type);
            SelectedCarChanged?.Invoke(SelectedCar.CarSprite);
            playerProgress.PlayerCarData.SelectedCarType = type;
        }
        

        public bool IsPlayerOwnCar(CarType type) => 
            playerProgress.PlayerCarData.OwnedItemsCar.Contains(type);

        public bool IsCoinsEnoughFor(int itemPrice) => CoinsAmount >= itemPrice;

        private CarStoreItemConfig GetValueCarItem(CarType type) => 
            staticDataService.StoreItemsCatalog.CarItems.GetValueOrDefault(type);
    }
}