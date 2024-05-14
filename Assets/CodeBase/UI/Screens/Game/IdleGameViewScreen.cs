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

        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            bestTimeText.text = presenter.BestTime;
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
            Hide();
            presenter.StartGame();
        }

        private void OnOpenSkinsShop()
        {
            presenter.OpenShop();
        }

        private void OnOpenLeaderboard() => 
            presenter.OpenLeaderboard();
    }
}