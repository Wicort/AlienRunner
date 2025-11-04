
using Assets._Project._scripts._core.Events;
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
            RoadGenerator.Instance.StopMoving();

            EventBus.Instance.Subscribe<BossKilledEvent>(OnBossKilled);
        }

        public void Exit()
        {
            EventBus.Instance.Unsubscribe<BossKilledEvent>(OnBossKilled);
        }

        private void OnBossKilled(BossKilledEvent @event)
        {
            Debug.Log("Boss killed! Transition to Hub.");

            _stateMachine.StateData.UIController.FadeOut(onComplete: () =>
            {
                Debug.Log("On fade complete");
                _stateMachine.Enter<HubState>();
            });
        }
    }
}
