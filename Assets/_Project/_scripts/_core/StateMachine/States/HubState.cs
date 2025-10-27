using Assets._Project._scripts.HUD;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine.States
{
    public class HubState : IState
    {

        private readonly GameStateMachine _stateMachine;

        private StateMachineData _data => _stateMachine.StateData;

        public HubState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter HubState");
            RoadGenerator.Instance.DisableLevel();
            _data.Hub.ShowHub();
            _data.Player.transform.SetParent(_data.Hub.transform);
            

            _data.UIController.FadeIn();
            _data.UIController.SwitchTo(UIController.UIMode.HubMode);
        }

        public void Exit()
        {
        }
    }
}
