using System;
using System.Collections.Generic;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.UI.Screens.Shop
{
    public interface IShopPresenter
    {
        event Action ChangedCoinsAmount;
        IReadOnlyCollection<CarShopItemConfig> SkinItems { get; set; }
        void InitializeShopItems();
        void Subscribe();
        void Unsubscribe();
    }
}