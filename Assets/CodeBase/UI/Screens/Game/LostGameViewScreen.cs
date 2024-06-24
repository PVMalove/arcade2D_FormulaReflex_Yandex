using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Game
{
    public class LostGameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Text bestTimeText;
        
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
            restartGameButton.onClick.AddListener(OnRestartGame);
            bestTimeText.text = presenter.BestTime;
        }

        protected override void UnsubscribeUpdates()
        {
            if (presenter is null) return;
            restartGameButton.onClick.RemoveListener(OnRestartGame);
        }

        private void OnRestartGame()
        {
            Hide();
            presenter.RestartGame();
        }
    }
}