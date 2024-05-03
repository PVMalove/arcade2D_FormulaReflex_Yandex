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
            restartGameButton.onClick.AddListener(OnRestartGame);
            bestTimeText.text = presenter.BestTime;
        }

        protected override void UnsubscribeUpdates()
        {
            restartGameButton.onClick.RemoveListener(OnRestartGame);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
        }

        private void OnRestartGame()
        {
            Hide();
            presenter.RestartGame();
        }
    }
}