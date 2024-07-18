using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.UI.Popup.Service
{
    public interface IPopupService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        
        void Cleanup();
        void InitializePresenter();
        void ShowCoinShop();
        void ShowRestorePurchase();
    }
}