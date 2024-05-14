using System;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Service;
using CodeBase.UI.Screens.Shop.Item;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Shop
{
    public class ShopViewScreen : ScreenBase<IShopPresenter>
    {
        [SerializeField] private Button closeScreenButton;
        [SerializeField] private ShopItemsPresenter shopItemList;
        
        private IShopPresenter presenter;

        protected override void Initialize(IShopPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            presenter.InitializeShopItems();
            shopItemList.SetShopItems(presenter.SkinItems);
            
            presenter.Subscribe();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            closeScreenButton.onClick.AddListener(CloseScreen);
        }

        protected override void UnsubscribeUpdates()
        {   
            base.UnsubscribeUpdates();
            presenter.Unsubscribe();
            shopItemList.Cleanup();
            closeScreenButton.onClick.RemoveListener(CloseScreen);
        }

        private void CloseScreen()
        {
            Hide();
        }
    }
}