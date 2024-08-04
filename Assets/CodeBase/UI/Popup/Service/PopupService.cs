using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.UI.Popup.Base;
using CodeBase.UI.Popup.CoinShop;
using CodeBase.UI.Popup.RestorePurchase;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Popup.Service
{
    public class PopupService : IPopupService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        private readonly IFrameSupplier<PopupName, UnityFrame> supplier;

        private ICoinShopPresenter coinShopPresenter;
        private IRestorePurchasePresenter restorePurchasePresenter;
        
        public PopupService(IFrameSupplier<PopupName, UnityFrame> supplier)
        {
            this.supplier = supplier;
        }

        public void InitializePresenter()
        {
            coinShopPresenter = new CoinShopPresenter(
                AllServices.Container.Single<IPersistentProgressService>()
                );
            
            restorePurchasePresenter = new RestorePurchasePresenter(
                AllServices.Container.Single<IPersistentProgressService>(),
                AllServices.Container.Single<ISaveService>()
                );
        }

        public void ShowCoinShop()
        {
            if (supplier.LoadFrame(PopupName.COIN_SHOP) is not CoinShopView view) return;
            view.Show(coinShopPresenter);
        }

        public void ShowRestorePurchase()
        {
            if (supplier.LoadFrame(PopupName.RESTORE_PURCHASE) is not RestorePurchaseView view) return;
            view.Show(restorePurchasePresenter);
        }
        
        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}