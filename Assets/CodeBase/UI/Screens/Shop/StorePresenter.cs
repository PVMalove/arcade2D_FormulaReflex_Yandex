using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.UI.Screens.Shop
{
    public class StorePresenter : IStorePresenter
    {
        public event Action ChangedCoinsAmount;
        
        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticDataService;
        
        public string CoinsAmount => 
            progressService.CoinsAmount.ToString();

        public StorePresenter(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            this.progressService = progressService;
            this.staticDataService = staticDataService;
        }
        
        public IReadOnlyCollection<CarStoreItemConfig> CarItems{ get; set; }
        
        public void InitializeShopItems()
        {
            CarItems = staticDataService.StoreItemsCatalog.CarItems.Values.ToList().AsReadOnly();
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