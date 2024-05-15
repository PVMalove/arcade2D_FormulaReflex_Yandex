using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IPersistentProgressService : IService
    {
        event Action CoinsAmountChanged;
        event Action<Sprite> SelectedCarChanged;
        
        int CoinsAmount { get; }
        CarStoreItemConfig SelectedCar { get; }
        PlayerProgress GetProgress();
        
        void Initialize(PlayerProgress progress);
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        void OpenCarItem(CarType type);
        bool IsCoinsEnoughFor(int itemPrice);
        bool IsPlayerOwnCar(CarType type);
        void SelectedCarItem(CarType type);
    }
}