using Assets._Project._scripts._core.Events;
using System;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class RunState : IState
    {
        private GameStateMachine _stateMachine;

        public RunState(GameStateMachine stateMachine) 
        { 
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter RunState");
            _stateMachine.StateData.Player.GetComponent<PlayerController>().enabled = true;
            _stateMachine.StateData.UIController.SwitchTo(HUD.UIController.UIMode.MenuMode);
            _stateMachine.StateData.StartGamePosition = _stateMachine.StateData.Player.transform.position;
            _stateMachine.StateData.StartGameRotation = _stateMachine.StateData.Player.transform.rotation;

            EventBus.Instance.Subscribe<RunStartedEvent>(OnRunStarted);
            EventBus.Instance.Subscribe<PlayerDeathEvent>(OnPlayerDeath);
        }

        public void Exit()
        {
            _stateMachine.StateData.Player.GetComponent<PlayerController>().enabled = false;

            EventBus.Instance.Unsubscribe<RunStartedEvent>(OnRunStarted);
            EventBus.Instance.Unsubscribe<PlayerDeathEvent>(OnPlayerDeath);
        }

        private void OnRunStarted(RunStartedEvent @event)
        {
            _stateMachine.StateData.UIController.SwitchTo(HUD.UIController.UIMode.GameplayMode);
            CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
            RoadGenerator.Instance.StartLevel();
            // _stateMachine.Enter<DeathState>();
        }

        private void OnPlayerDeath(PlayerDeathEvent @event)
        {
            _stateMachine.Enter<DeathState>();
            //_stateMachine.StateData.UIController.SwitchTo(HUD.UIController.UIMode.GameplayMode);
            //CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
        }
    }
}
