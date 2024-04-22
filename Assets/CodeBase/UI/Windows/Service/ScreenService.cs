using System.Threading;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.Base;
using CodeBase.UI.Windows.GameCanvas;

namespace CodeBase.UI.Windows.Service
{
    public class ScreenService : IScreenService
    {
        private readonly IFrameSupplier<ScreenName, UnityFrame> supplier;
        private readonly CancellationTokenSource ctn;

        public ScreenService(IFrameSupplier<ScreenName, UnityFrame> supplier)
        {
            this.supplier = supplier;
        }

        public void ShowGameView()
        {
            if (supplier.LoadFrame(ScreenName.GAME) is GameViewScreen gameView)
            {
                IGamePresenter presenter = new GamePresenter();
                gameView.Show(presenter);
            }
        }
    }
}