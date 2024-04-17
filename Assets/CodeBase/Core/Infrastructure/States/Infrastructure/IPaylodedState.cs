namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}