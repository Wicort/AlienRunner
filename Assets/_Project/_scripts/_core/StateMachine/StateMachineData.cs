using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine
{
    public class StateMachineData
    {
        private PlayerComponent _playerComponent;
        private HubController _hubController;
        private UIController _uiController;

        public PlayerComponent Player => _playerComponent;
        public HubController Hub => _hubController;
        public UIController UIController => _uiController;
        public Vector3 StartGamePosition;
        public Quaternion StartGameRotation;


        public StateMachineData(PlayerComponent playerComponent, HubController hubController, UIController uiController)
        {
            _playerComponent = playerComponent;
            _uiController = uiController;
            _hubController = hubController;
        }
    }
}
