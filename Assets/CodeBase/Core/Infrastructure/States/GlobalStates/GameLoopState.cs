using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using YG;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoopState : IState
    {
        private readonly ILogService log;

        public GameLoopState(ILogService log)
        {
            this.log = log;
        }

        public void Enter()
        {
            log.LogState("Enter", this);
            YandexGame.GameReadyAPI();
        }

        public void Exit()
        {
            log.LogState("Enter", this);
        }
    }
}