using Assets._Project._scripts._core.StateMachine;
using Assets._Project._scripts.HUD;
using Assets._Project._scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project._scripts._core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerSpawnPoint;
        [SerializeField] private HubController _hubController;
        [SerializeField] private UIController _uiController;

        private void Awake()
        {
            GameObject player = SpawnPlayer();
            _uiController.Initialize(player.GetComponent<PlayerComponent>().HealthUI);
            GameStateMachine stateMachine = 
                new GameStateMachine(
                    player.GetComponent<PlayerComponent>(), 
                    _hubController, 
                    _uiController);
        }

        private GameObject SpawnPlayer()
        {
            return Instantiate(_playerPrefab, _playerSpawnPoint.transform);
        }
    }
}
