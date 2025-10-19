using UnityEngine;

namespace Assets._Project._scripts.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private Canvas _healthUI;

        public Canvas HealthUI => _healthUI;

    }
}
