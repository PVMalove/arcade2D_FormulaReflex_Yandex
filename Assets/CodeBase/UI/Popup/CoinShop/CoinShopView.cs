using CodeBase.UI.Popup.Base;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.UI.Popup.CoinShop
{
    public class CoinShopView : PopupBase<ICoinShopPresenter>
    {
        [SerializeField] private Button closeScreenButton;
        
        [SerializeField] private Button videoAdRevive;
        [SerializeField] private GameObject shopYAN;
        [SerializeField] private PurchaseYG purchasePrefab;
        
        private ICoinShopPresenter presenter;
        
        protected override void Initialize(ICoinShopPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;

            if (YandexGame.auth)
            {
                UpdatePurchases();
                shopYAN.SetActive(true);
            }
            
            YandexGame.PurchaseSuccessEvent += PurchaseOnContinue;
            YandexGame.CloseVideoEvent += RewardedOnContinue;
            YandexGame.PurchaseFailedEvent += PurchaseFailed;
            
            videoAdRevive.onClick.AddListener(OpenRewardAd);
            closeScreenButton.onClick.AddListener(CloseScreen);
        }

        protected override void UnsubscribeUpdates()
        {            
            base.UnsubscribeUpdates();
            if (presenter is null) return;

            YandexGame.PurchaseSuccessEvent -= PurchaseOnContinue;
            YandexGame.CloseVideoEvent -= RewardedOnContinue;
            YandexGame.PurchaseFailedEvent -= PurchaseFailed;
            
            videoAdRevive.onClick.RemoveListener(OpenRewardAd);
            closeScreenButton.onClick.RemoveListener(CloseScreen);
        }
        
        private void OpenRewardAd()
        {
            YandexGame.RewVideoShow(0);
        }
        
        private void RewardedOnContinue()
        {
            presenter.AddCoins(100);
            Hide();
        }
        
        private void PurchaseOnContinue(string obj)
        {
            presenter.AddCoins(1000);
            Hide();
        }
        
        private void PurchaseFailed(string obj)
        {
            Debug.Log($"[YandexGame] Purchase failed {obj}");
        }
        
        private void UpdatePurchases()
        {
            purchasePrefab.data = YandexGame.purchases[0];
            purchasePrefab.UpdateEntries();
        }
        
        private void CloseScreen()
        {
            Hide();
        }
    }
}