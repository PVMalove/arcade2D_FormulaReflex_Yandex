using System;
using CodeBase.UI.Popup.Base;
using CodeBase.UI.Popup.CoinShop;
using CodeBase.UI.Popup.RestorePurchase;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Popup.Supplier
{
    public class PopupSupplier : FrameSupplier<PopupName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public PopupSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(PopupName key)
        {
            switch (key)
            {
                case PopupName.None:
                    break;
                case PopupName.COIN_SHOP:
                    CoinShopView coinShopView = uiFactory.CreateCoinShopView();
                    coinShopView.transform.SetParent(uiFactory.UIRoot.ContainerPopup, false);
                    coinShopView.name = "CoinShopView";
                    return coinShopView;
                case PopupName.RESTORE_PURCHASE:
                    RestorePurchaseView restorePurchaseView = uiFactory.CreateRestorePurchaseView();
                    restorePurchaseView.transform.SetParent(uiFactory.UIRoot.ContainerPopup, false);
                    restorePurchaseView.name = "RestorePurchaseView";
                    return restorePurchaseView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}