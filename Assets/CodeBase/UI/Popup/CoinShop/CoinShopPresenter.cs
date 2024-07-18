using CodeBase.Core.Services.ProgressService;

namespace CodeBase.UI.Popup.CoinShop
{
    public class CoinShopPresenter : ICoinShopPresenter
    {
        private readonly IPersistentProgressService progressService;

        public CoinShopPresenter(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }
        
        public void AddCoins(int coinsAmount)
        {
            progressService.AddCoins(coinsAmount);
        }
    }
}