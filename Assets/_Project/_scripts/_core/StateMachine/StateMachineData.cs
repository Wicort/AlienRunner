using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using UnityEngine;

namespace Assets._Project._scripts._core.StateMachine
{
    public class StateMachineData
    {
        private PlayerComponent _playerComponent;
        private UIController _uiController;

        public PlayerComponent Player => _playerComponent;
        public UIController UIController => _uiController;
        public Vector3 StartGamePosition;
        public Quaternion StartGameRotation;


        public StateMachineData(PlayerComponent playerComponent, UIController uiController)
        {
            _playerComponent = playerComponent;
            _uiController = uiController;
        }
    }
}
