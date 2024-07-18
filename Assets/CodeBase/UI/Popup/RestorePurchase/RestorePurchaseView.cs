using CodeBase.UI.Popup.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popup.RestorePurchase
{
    public class RestorePurchaseView : PopupBase<IRestorePurchasePresenter>
    {
        [SerializeField] private Button closeScreenButton;
        
        private IRestorePurchasePresenter presenter;
        
        protected override void Initialize(IRestorePurchasePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            presenter.AddCoins(1000);
            closeScreenButton.onClick.AddListener(CloseScreen);
        }
        
        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            if (presenter is null) return;

            closeScreenButton.onClick.RemoveListener(CloseScreen);
        }
       
        private void CloseScreen()
        {
            Hide();
        }
    }
}