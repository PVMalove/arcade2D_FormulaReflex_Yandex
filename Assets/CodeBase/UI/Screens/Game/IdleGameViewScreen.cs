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
        [SerializeField] private Text coinsAmountText;
        [SerializeField] private Image CarSprite;

        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            bestTimeText.text = presenter.BestTime;
            presenter.Subscribe();
            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            //presenter.ChangedSelectedCar += OnSelectedCarChanged;
            OnCoinsAmountChanged();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.AddListener(OnOpenLeaderboard);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.RemoveListener(OnOpenLeaderboard);
        }

        private void OnStartGame()
        {
            presenter.Unsubscribe();
            presenter.ChangedCoinsAmount -= OnCoinsAmountChanged;
            //presenter.ChangedSelectedCar -= OnSelectedCarChanged;
            Hide();
            presenter.StartGame();
        }

        private void OnOpenSkinsShop()
        {
            presenter.OpenShop();
        }

        private void OnOpenLeaderboard() => 
            presenter.OpenLeaderboard();
        
        private void OnCoinsAmountChanged() => 
            coinsAmountText.text = presenter.CoinsAmount;
        
        private void OnSelectedCarChanged(Sprite view) => 
            CarSprite.sprite = view;
    }
}