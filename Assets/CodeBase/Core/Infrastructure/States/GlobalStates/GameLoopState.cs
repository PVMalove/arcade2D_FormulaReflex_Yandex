using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.Popup.Base;
using CodeBase.UI.Screens.Base;
using CodeBase.UI.Services.Infrastructure;
using YG;


namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoopState : IState
    {
        private readonly IFrameSupplier<HUDName, UnityFrame> hudSupplier;
        private readonly IFrameSupplier<ScreenName, UnityFrame> screenSupplier;
        private readonly IFrameSupplier<PopupName, UnityFrame> popupSupplier;
        private readonly ILogService log;

        public GameLoopState(IFrameSupplier<HUDName,UnityFrame> hudSupplier,
            IFrameSupplier<ScreenName, UnityFrame> screenSupplier, 
            IFrameSupplier<PopupName, UnityFrame> popupSupplier, 
            ILogService log)
        {
            this.hudSupplier = hudSupplier;
            this.screenSupplier = screenSupplier;
            this.popupSupplier = popupSupplier;
            this.log = log;
        }

        public void Enter()
        {
            log.LogState("Enter", this);
            YandexGame.GameReadyAPI();
        }

        public void Exit()
        {
            hudSupplier.AllUnloadFrame();
            screenSupplier.AllUnloadFrame();
            popupSupplier.AllUnloadFrame();
            log.LogState("Enter", this);
        }
    }
}