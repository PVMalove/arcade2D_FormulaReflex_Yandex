using CodeBase.UI.Screens.Base;
using CodeBase.UI.Screens.Service;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Game
{
    public class LostGameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Text bestTimeText;
        [SerializeField] private TimerAds timerAds;

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
            timerAds.EndShowAdCountdown += OnRestartGame;
            restartGameButton.onClick.AddListener(OnRestartGame);
            bestTimeText.text = presenter.BestTime;
        }

        protected override void UnsubscribeUpdates()
        {
            if (presenter is null) return;
            timerAds.EndShowAdCountdown -= OnRestartGame;
            restartGameButton.onClick.RemoveListener(OnRestartGame);
        }

        private void OnRestartGame()
        {
            timerAds.CheckTimerAd();
            
            if (timerAds.IsAdActive)
            {
                timerAds.StartAdCountdown();
                return;
            }
            
            Hide();
            presenter.RestartGame();
        }
    }
}