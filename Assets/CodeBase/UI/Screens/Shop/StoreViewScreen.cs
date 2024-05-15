using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Shop.Item;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Shop
{
    public class StoreViewScreen : ScreenBase<IStorePresenter>
    {
        [SerializeField] private Text coinsAmountText;
        [SerializeField] private Button closeScreenButton;
        [SerializeField] private ShopItemsPresenter shopItemList;
        
        private IStorePresenter presenter;

        protected override void Initialize(IStorePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            presenter.InitializeShopItems();
            shopItemList.SetShopItems(presenter.CarItems);
            
            presenter.Subscribe();
            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            OnCoinsAmountChanged();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            closeScreenButton.onClick.AddListener(CloseScreen);
        }

        protected override void UnsubscribeUpdates()
        {   
            base.UnsubscribeUpdates();
            shopItemList.Cleanup();
            closeScreenButton.onClick.RemoveListener(CloseScreen);
        }

        private void OnCoinsAmountChanged() => 
            coinsAmountText.text = presenter.CoinsAmount;
        
        private void CloseScreen()
        {
            presenter.Unsubscribe();
            Hide();
        }
    }
}