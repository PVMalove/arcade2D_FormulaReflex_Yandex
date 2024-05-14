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
        public CarShopItemConfig SelectedCarView { get; private set; }
        public PlayerProgress GetProgress() => playerProgress;

        private PlayerProgress playerProgress;
        private List<CarShopItemConfig> ownedCarView;

        private readonly IStaticDataService staticDataService;

        public PersistentProgressService(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public void Initialize(PlayerProgress progress)
        {
            playerProgress = progress;

            if (progress.playerCarData.ItemsCar is null) 
                progress.playerCarData.ItemsCar = new List<CarViewType> { CarViewType.f1_car_0 };
            
            ownedCarView = progress.playerCarData.ItemsCar
                .Select(type => staticDataService.ShopItemsCatalog.CarItems.GetValueOrDefault(type))
                .ToList();
            
            SelectedCarView = ownedCarView.Find(x => x.Type == progress.playerCarData.SelectedCarType);
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
        
        public void OpenCarView(CarViewType type)
        {
            if (IsPlayerOwnCarView(type))
            {
                //Debug.LogError($"Player already have such skin - {circleDataReference}");
                return;
            }
            //
            // ownedCircleHeroesReferences.Add(circleDataReference);
            // playerProgress.PlayerItemsData.SkinGuids.Add(circleDataReference.AssetGUID);
        }
 
        public bool IsPlayerOwnCarView(CarViewType type) => 
            playerProgress.playerCarData.ItemsCar.Contains(type);
        
        public bool IsCoinsEnoughFor(int itemPrice) => playerProgress.CoinData.CoinsAmount >= itemPrice;
    }

}