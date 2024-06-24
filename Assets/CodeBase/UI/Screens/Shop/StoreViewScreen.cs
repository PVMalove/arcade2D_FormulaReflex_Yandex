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
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            presenter.Subscribe();
            presenter.InitializeShopItems();
            shopItemList.SetShopItems(presenter.CarItems);
            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            OnCoinsAmountChanged();
            closeScreenButton.onClick.AddListener(CloseScreen);
        }

        protected override void UnsubscribeUpdates()
        {   
            base.UnsubscribeUpdates();
            if (presenter is null) return;
            shopItemList.Cleanup();
            closeScreenButton.onClick.RemoveListener(CloseScreen);
            presenter.Unsubscribe();
        }

        private void OnCoinsAmountChanged() => 
            coinsAmountText.text = presenter.CoinsAmount;
        
        private void CloseScreen()
        {
            Hide();
        }
    }
}