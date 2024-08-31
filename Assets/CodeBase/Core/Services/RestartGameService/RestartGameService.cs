using CodeBase.Core.Infrastructure.States.Infrastructure;


namespace CodeBase.Core.Services.RestartGameService
{
    public class RestartGameService : IRestartGameService
    {
        private readonly GlobalStateMachine globalStateMachine;
        
        public RestartGameService(GlobalStateMachine globalStateMachine)
        {
            this.globalStateMachine = globalStateMachine;
        }
        
        public void RestartGame()
        {
            globalStateMachine.Start();
        }
    }
}