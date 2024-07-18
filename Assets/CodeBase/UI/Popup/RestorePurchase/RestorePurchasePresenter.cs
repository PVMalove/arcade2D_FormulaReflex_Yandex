using CodeBase.Core.Services.ProgressService;

namespace CodeBase.UI.Popup.RestorePurchase
{
    public class RestorePurchasePresenter : IRestorePurchasePresenter
    {
        private readonly IPersistentProgressService progressService;

        public RestorePurchasePresenter(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }
        
        public void AddCoins(int coinsAmount)
        {
            progressService.AddCoins(coinsAmount);
        }
    }
}