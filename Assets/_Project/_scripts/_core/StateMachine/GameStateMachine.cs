using Assets._Project._scripts._core.StateMachine.States;
using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine
{
    public class GameStateMachine
    {
        private List<IExitableState> _states;
        private IExitableState _currentState;
        private StateMachineData _stateMachineData;

        public IExitableState CurrentState => _currentState;
        public StateMachineData StateData => _stateMachineData;

        public GameStateMachine(PlayerComponent playerComponent, UIController uiController)
        {
            _states = new List<IExitableState>()
            {
                new BootStrapState(this, playerComponent, uiController),
                new RunState(this),
                new BossState(this),
                new DeathState(this, playerComponent, uiController),
            };

            Enter<BootStrapState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState: class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();

            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState: class, IExitableState
        {
            return _states.FirstOrDefault(state => state is TState) as TState;
        }

        public void SetStateMachineData(StateMachineData data)
        {
            _stateMachineData = data;
        }
    }
}
