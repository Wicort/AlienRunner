using Assets._Project._scripts._core.StateMachine.States;
using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine
{
    public class DeathState : IState
    {
        private GameStateMachine _stateMachine;
        private PlayerComponent _playerComponent;
        private UIController _uiController;

        public DeathState(GameStateMachine gameStateMachine, PlayerComponent playerComponent, UIController uiController)
        {
            _stateMachine = gameStateMachine;
            _playerComponent = playerComponent;
            _uiController = uiController;
        }

        public void Enter()
        {
            Debug.Log("Enter DeathState");
            RoadGenerator.Instance.ResetLevel();
            CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.MenuCamera);
            _stateMachine.StateData.Player.transform.position = _stateMachine.StateData.StartGamePosition;
            _stateMachine.StateData.Player.transform.rotation = _stateMachine.StateData.StartGameRotation;
            _stateMachine.Enter<RunState>();
        }

        public void Exit()
        {
            
        }
    }
}
