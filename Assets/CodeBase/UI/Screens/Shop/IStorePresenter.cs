using System;
using System.Collections.Generic;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.UI.Screens.Shop
{
    public interface IStorePresenter
    {
        event Action ChangedCoinsAmount;
        IReadOnlyCollection<CarStoreItemConfig> CarItems { get; set; }
        string CoinsAmount { get; }
        void InitializeShopItems();
        void Subscribe();
        void Unsubscribe();
    }
}