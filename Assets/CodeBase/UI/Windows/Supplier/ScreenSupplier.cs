using System;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.Base;
using CodeBase.UI.Windows.GameCanvas;

namespace CodeBase.UI.Windows.Supplier
{
    public class ScreenSupplier : FrameSupplier<ScreenName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public ScreenSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(ScreenName key)
        {
            switch (key)
            {
                case ScreenName.None:
                    break;
                case ScreenName.GAME:
                    GameViewScreen gameView = uiFactory.CreateGameView();
                    gameView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    gameView.name = "GameView";
                    return gameView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}