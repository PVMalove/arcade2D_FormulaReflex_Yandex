using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Game
{
    public class EndedGameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Text bestTimeText;
        [SerializeField] private Text resultTimeText;
        
        private IGamePresenter presenter;
        
        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            bestTimeText.text = presenter.BestTime;
            resultTimeText.text = presenter.TimeDiff;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            restartGameButton.onClick.AddListener(OnRestartGame);
        }

        protected override void UnsubscribeUpdates()
        {
            restartGameButton.onClick.RemoveListener(OnRestartGame);
        }

        private void OnRestartGame()
        {
            Hide();
            presenter.RestartGame();
        }
    }
}