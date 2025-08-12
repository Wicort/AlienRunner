using Assets._Project._scripts._core.Reactive;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._scripts.Player
{
    public class SliderVlew : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private ReactiveVariable<float> _current;
        private ReactiveVariable<float> _max;

        public void Initialize(ReactiveVariable<float> current, ReactiveVariable<float> max)
        {
            _current = current;
            _max = max; 

            _current.Changed += OnHealthChanged;

            UpdateValue(_current.Value, _max.Value);
        }

        private void OnDestroy()
        {
            _current.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(float arg1, float newValue) => UpdateValue(newValue, _max.Value);

        private void UpdateValue(float currentValue, float maxValue) => _slider.value = currentValue / maxValue;
    }
}
