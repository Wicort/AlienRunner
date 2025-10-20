
using Assets._Project._scripts._core.Events;
using Assets._Project._scripts._core.Events.Structs;
using System;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class BossState : IState
    {
        private GameStateMachine _stateMachine;
        public BossState(GameStateMachine gameStateMachine) 
        { 
            _stateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter BossState");
            _stateMachine.StateData.Player.GetComponent<PlayerController>().enabled = false;

            EventBus.Instance.Subscribe<BossKilledEvent>(OnBossKilled);
        }

        public void Exit()
        {
            EventBus.Instance.Unsubscribe<BossKilledEvent>(OnBossKilled);
        }

        private void OnBossKilled(BossKilledEvent @event)
        {
            _stateMachine.Enter<RunState>();
        }
    }
}
