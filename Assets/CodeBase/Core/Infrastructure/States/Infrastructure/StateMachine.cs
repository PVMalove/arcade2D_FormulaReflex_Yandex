using System;
using System.Collections.Generic;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public abstract class StateMachine : IStateMachine
    {
        public event Action OnExitState;

        private readonly Dictionary<Type, IExitableState> registeredStates = new Dictionary<Type, IExitableState>();
        private IExitableState currentState;

        public IExitableState CurrentState => currentState;

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState =>
            registeredStates.Add(typeof(TState), state);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            if (currentState is not null)
            {
                currentState.Exit();
                OnExitState?.Invoke();
            }

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            registeredStates[typeof(TState)] as TState;
    }
}