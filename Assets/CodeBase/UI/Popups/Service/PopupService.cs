using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Infrastructure;


namespace CodeBase.UI.Popups.Service
{
    public sealed class PopupService : IPopupService
    {
        private readonly IFrameSupplier<PopupName, UnityFrame> supplierAsync;
        private readonly ConcurrentDictionary<PopupName, UnityFrame> activePopups = new ConcurrentDictionary<PopupName, UnityFrame>();
        
        public PopupService(IFrameSupplier<PopupName, UnityFrame> supplier)
        {
            supplierAsync = supplier;
        }
        
        public void ShowPopup<TInitializeData>(PopupName name, TInitializeData initializeData)
        {
            if(IsPopupActive(name)) return;
                
            UnityFrame frame = supplierAsync.LoadFrame(name);
            activePopups.TryAdd(name, frame);
            
            if (frame is PopupBase<TInitializeData> popupView)
            {
                popupView.Show(initializeData);
            }
            else
            {
                throw new InvalidCastException("Received object is not a PopupBase instance");
            }
        }
        
        public void HidePopup(PopupName name)
        {   
            supplierAsync.UnloadFrame(activePopups[name]);
            activePopups.TryRemove(name, out _);
        }

        public bool IsPopupActive(PopupName key) => 
            activePopups.ContainsKey(key);
    }
}