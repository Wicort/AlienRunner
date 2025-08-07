using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._scripts.Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private Health _health;

        public void Initialize(Health health)
        {
            _health = health;
            _health.Changed += OnHealthChanged;
            UpdateValue(health.Current);
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(float arg1, float newValue) => UpdateValue(newValue);

        private void UpdateValue(float value) => _slider.value = value / _health.Max;
    }
}
