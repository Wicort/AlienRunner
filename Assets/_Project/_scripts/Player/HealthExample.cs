using UnityEngine;

namespace Assets._Project._scripts.Player
{
    public class HealthExample: MonoBehaviour
    {
        [SerializeField] private HealthBar _healthView;
        private Health _health;

        private void Awake()
        {
            _health = new Health(100f, 100f);

            _healthView.Initialize(_health);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
            {
                _health.Reduce(10f);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2)) 
            {
                _health.Add(10f);
            }
        }
    }
}
