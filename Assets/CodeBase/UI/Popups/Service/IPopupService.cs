using System.Threading.Tasks;
using CodeBase.UI.Popups.Base;

namespace CodeBase.UI.Popups.Service
{
    public interface IPopupService
    {
        void ShowPopup<TInitializeData>(PopupName key, TInitializeData initializeData);
        bool IsPopupActive(PopupName key);
        void HidePopup(PopupName name);
    }
}