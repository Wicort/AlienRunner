using Assets._Project._scripts._core.Events;
using System;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class RunState : IState
    {
        private GameStateMachine _stateMachine;

        private StateMachineData _data => _stateMachine.StateData;

        public RunState(GameStateMachine stateMachine) 
        { 
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter RunState");

            if (RoadGenerator.Instance != null)
            {
                RoadGenerator.Instance.gameObject.SetActive(true);
                RoadGenerator.Instance.Initialize();
            }

            _data.Player.GetComponent<PlayerController>().Initialize();
            _data.UIController.SwitchTo(HUD.UIController.UIMode.MenuMode);
            
            if (CameraSwitcher.Instance != null) 
                CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.MenuCamera);

            EventBus.Instance.Subscribe<RunStartedEvent>(OnRunStarted);
            EventBus.Instance.Subscribe<PlayerDeathEvent>(OnPlayerDeath);
            EventBus.Instance.Subscribe<BossStartEvent>(OnBossStarted);
        }

        public void Exit()
        {
            _data.Player.GetComponent<PlayerController>().enabled = false;

            EventBus.Instance.Unsubscribe<RunStartedEvent>(OnRunStarted);
            EventBus.Instance.Unsubscribe<PlayerDeathEvent>(OnPlayerDeath);
            EventBus.Instance.Unsubscribe<BossStartEvent>(OnBossStarted);
            
        }

        private void OnRunStarted(RunStartedEvent @event)
        {
            _data.UIController.SwitchTo(HUD.UIController.UIMode.GameplayMode);
            CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
            RoadGenerator.Instance.StartLevel();
        }

        private void OnPlayerDeath(PlayerDeathEvent @event)
        {
            _stateMachine.Enter<DeathState>();
        }

        private void OnBossStarted(BossStartEvent @event)
        {
            _stateMachine.Enter<BossState>();
        }
    }
}
