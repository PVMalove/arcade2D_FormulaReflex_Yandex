using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IPersistentProgressService : IService
    {
        event Action CoinsAmountChanged;
        CarShopItemConfig SelectedCarView { get; }
        PlayerProgress GetProgress();
        void Initialize(PlayerProgress progress);
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        bool IsCoinsEnoughFor(int itemPrice);
        void OpenCarView(CarViewType type);
        bool IsPlayerOwnCarView(CarViewType type);
    }
}