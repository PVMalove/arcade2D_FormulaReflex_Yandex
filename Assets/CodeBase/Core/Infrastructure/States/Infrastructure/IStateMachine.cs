using System;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
        void RegisterState<TState>(TState state) where TState : IExitableState;
        event Action OnExitState;
        IExitableState CurrentState { get; }
    }
}