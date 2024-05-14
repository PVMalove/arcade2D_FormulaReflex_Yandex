using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.UI.Screens.Shop
{
    public class ShopPresenter : IShopPresenter
    {
        public event Action ChangedCoinsAmount;
        
        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticDataService;

        public ShopPresenter(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            this.progressService = progressService;
            this.staticDataService = staticDataService;
        }
        
        public IReadOnlyCollection<CarShopItemConfig> SkinItems{ get; set; }
        
        public void InitializeShopItems()
        {
            SkinItems = staticDataService.ShopItemsCatalog.CarItems.Values.ToList().AsReadOnly();
        }
        
        public void Subscribe()
        {
            progressService.CoinsAmountChanged += OnCoinsAmountChanged;
        }

        public void Unsubscribe()
        {
            progressService.CoinsAmountChanged -= OnCoinsAmountChanged;
        }
        
        private void OnCoinsAmountChanged()
        {
            ChangedCoinsAmount?.Invoke();
        }
    }
}