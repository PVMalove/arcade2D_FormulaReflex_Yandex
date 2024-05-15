using System;
using System.Collections.Generic;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.PoolService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;


namespace CodeBase.UI.Screens.Shop.Item
{
    public class ShopItemsPresenter : MonoBehaviour
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private Transform poolContainer;
        
        private readonly List<ShopItemView> activeViews = new List<ShopItemView>();
        
        private IPersistentProgressService progressService;
        private IAssetProvider assetProvider;
        private IGameFactory gameFactory;
        private IPoolFactory poolFactory;
        private IStaticDataService staticDataService;
        private ObjectPool<ShopItemView> objectPool;

        public void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService,
            IPoolFactory poolFactory)
        {
            this.progressService = progressService;
            this.staticDataService = staticDataService;
            this.poolFactory = poolFactory;
        }
        
        public void Initialize()
        {
            objectPool = new ObjectPool<ShopItemView>(poolFactory);
            PoolObjectConfig poolConfig = staticDataService.GetPoolConfigByType(PoolObjectType.StoreItem);
            objectPool.Initialize(poolConfig.AssetReference, poolConfig.StartCapacity, poolConfig.Type, poolContainer);
        }
        
        public void SetShopItems(IEnumerable<CarStoreItemConfig> items)
        {
            foreach (CarStoreItemConfig storeItem in items)
            {
                ShopItemView viewItem = objectPool.Get(itemContainer.position, itemContainer);
                
                SetItem(viewItem, storeItem.CarSprite, storeItem.RequiredCoins,
                    () => BuyCarItem(storeItem.Type, storeItem.RequiredCoins),
                    () => SelectCarItem(storeItem.Type));
                
                if (!progressService.IsPlayerOwnCar(storeItem.Type))
                {
                    continue;
                }
                
                viewItem.Unlock();
                
                if (progressService.SelectedCar.Type == storeItem.Type)
                {
                    viewItem.Select();
                }
            }
        }

        private void BuyCarItem(CarType type, int price)
        {
            progressService.OpenCarItem(type);
            progressService.RemoveCoins(price);
        }
        
        private void SelectCarItem(CarType type)
        {
            UnselectAllItems();
            progressService.SelectedCarItem(type);
        }

        
        public void Cleanup()
        {
            foreach (ShopItemView item in activeViews)
            {
                objectPool.Return(item);
            }
        
            activeViews.Clear();
        }
        
        private void SetItem(ShopItemView viewItem, Sprite itemIcon, int requiredCoinsAmount,
            Action onBuyButtonClicked, Action onSelectButtonClicked)
        {
            viewItem.gameObject.SetActive(false);
        
            viewItem.SetItem(itemIcon, requiredCoinsAmount,
                onBuyButtonClicked, onSelectButtonClicked);
            
            viewItem.Unselect();
            viewItem.Lock();
        
            viewItem.gameObject.SetActive(true);
            activeViews.Add(viewItem);
        }
        
        private void UnselectAllItems()
        {
            foreach (ShopItemView shopItemView in activeViews)
            {
                if (shopItemView.IsSelected)
                {
                    shopItemView.Unselect();
                }
            }
        }
    }
}