using CodeBase.Core.StaticData.UI.Shop;
using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;


namespace CodeBase.UI.Screens.Game
{
    public class IdleGameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button openSkinsShopButton;
        [SerializeField] private Button openLeaderboardButton;
        [SerializeField] private Text bestTimeText;
        [SerializeField] private GameObject bestTimeObject;
        [SerializeField] private Text coinsAmountText;
        
        [SerializeField] private GameObject notifyObject;
        
         
        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            presenter.Subscribe();

            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.AddListener(OnOpenLeaderboard);
            
            BestTimeChanged();
            OnCoinsAmountChanged();
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            if (presenter is null) return;
            presenter.ChangedCoinsAmount -= OnCoinsAmountChanged;
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.RemoveListener(OnOpenLeaderboard);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            presenter.Unsubscribe();
        }

        private void OnStartGame()
        {
            Hide();
            presenter.StartGame();
        }

        private void OnOpenSkinsShop()
        {
            presenter.OpenShop();
        }

        private void OnOpenLeaderboard() => 
            presenter.OpenLeaderboard();

        private void BestTimeChanged()
        {
            if (presenter.BestTime == "00.000")
            {
                bestTimeObject.gameObject.SetActive(false);
            }
            else
            {
                bestTimeObject.gameObject.SetActive(true);
                bestTimeText.text = presenter.BestTime;
            }
        }

        private void OnCoinsAmountChanged()
        {
            coinsAmountText.text = presenter.CoinsAmount;
            CheckNotify();
        }

        private void CheckNotify()
        {
            notifyObject.SetActive(false);
            for (int i = 0; i < presenter.SkinsData.Count; i++)
            {
                if (presenter.SkinsData.TryGetValue((CarType)i, out CarStoreItemConfig value))
                {
                    if (int.TryParse(presenter.CoinsAmount , out int coinsAmount))
                        if (coinsAmount>= value.RequiredCoins)
                        {
                            if (i == 0) continue;
                            notifyObject.SetActive(true);
                            return;
                        }
                }
            }
        }
    }
}