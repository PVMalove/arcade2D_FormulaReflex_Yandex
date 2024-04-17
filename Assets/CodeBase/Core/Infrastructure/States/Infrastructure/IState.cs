namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}