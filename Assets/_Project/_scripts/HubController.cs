using UnityEngine;

namespace Assets._Project._scripts
{
    public class HubController : MonoBehaviour
    {
        [SerializeField] private GameObject _hubObject;
        [SerializeField] private Transform _playerSpawnPoint;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        private void Awake()
        {
            HideHub();
        }

        public void ShowHub()
        {
            _hubObject.SetActive(true);
        }

        public void HideHub()
        {
            _hubObject.SetActive(false);
        }
    }
}
