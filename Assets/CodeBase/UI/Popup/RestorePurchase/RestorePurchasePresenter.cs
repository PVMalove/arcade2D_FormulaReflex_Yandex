using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;

namespace CodeBase.UI.Popup.RestorePurchase
{
    public class RestorePurchasePresenter : IRestorePurchasePresenter
    {
        private readonly IPersistentProgressService progressService;
        private readonly ISaveService saveService;

        public RestorePurchasePresenter(IPersistentProgressService progressService,
            ISaveService saveService)
        {
            this.progressService = progressService;
            this.saveService = saveService;
        }
        
        public void AddCoins(int coinsAmount)
        {
            progressService.AddCoins(coinsAmount);
            saveService.SaveProgress();
        }
    }
}