using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class BossState : IState
    {
        private GameStateMachine _gameStateMachine;
        public BossState(GameStateMachine gameStateMachine) 
        { 
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter BossState");
        }

        public void Exit()
        {
        }
    }
}
