using Assets._Project._scripts._core.Events;
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
            //CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.MenuCamera);
            EventBus.Instance.Subscribe<RunStartedEvent>(OnRunStarted);
        }

        public void Exit()
        {
            _stateMachine.StateData.Player.GetComponent<PlayerController>().enabled = false;
            EventBus.Instance.Unsubscribe<RunStartedEvent>(OnRunStarted);
        }

        private void OnRunStarted(RunStartedEvent @event)
        {
            _stateMachine.StateData.UIController.SwitchTo(HUD.UIController.UIMode.GameplayMode);
            CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
        }
    }
}
