using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class BootStrapState : IState
    {
        private GameStateMachine _stateMachine;

        private StateMachineData _data => _stateMachine.StateData;

        public BootStrapState(GameStateMachine gameStateMachine, PlayerComponent playerComponent, HubController hubController, UIController uiController)
        {
            _stateMachine = gameStateMachine;
            _stateMachine.SetStateMachineData(
                new StateMachineData(
                    playerComponent,
                    hubController,
                    uiController));
        }


        public void Enter()
        {
            Debug.Log("Enter BootStrapState");

            _data.Player.GetComponent<PlayerController>().enabled = false;
            _stateMachine.Enter<RunState>();
        }

        public void Exit()
        {
            
        }
    }
}
