using Assets._Project._scripts._core.Events;
using Assets._Project._scripts.HUD;
using System;
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
            _data.Player.GetComponent<PlayerController>().DisableControl();
            
            _data.Hub.ShowHub();

            _data.Player.transform.position = _data.Hub.PlayerSpawnPoint.transform.position;
            _data.Player.transform.rotation = _data.Hub.PlayerSpawnPoint.transform.rotation;

            _data.UIController.FadeIn();
            _data.UIController.SwitchTo(UIController.UIMode.HubMode);
            CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.HubHeroCamera);

            EventBus.Instance.Subscribe<GoToLevelEvent>(OnLevelStarted);
        }

        public void Exit()
        {
            EventBus.Instance.Unsubscribe<GoToLevelEvent>(OnLevelStarted);
        }

        private void OnLevelStarted(GoToLevelEvent @event)
        {
            Debug.Log(@event.Level);
        }
    }
}
