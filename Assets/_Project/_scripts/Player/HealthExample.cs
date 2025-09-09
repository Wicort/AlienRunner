using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts.Player
{
    public class HealthExample: MonoBehaviour
    {
        [SerializeField] private SliderVlew _healthView;
        private Health _health;

        private void Awake()
        {
            _health = new Health(100f, 100f);

            _healthView.Initialize(_health.Current, _health.Max);
        }

        private void OnEnable()
        {
            EventBus.Instance.Subscribe<HealPickedEvent>(OnHealthPicked);
        }

        private void OnHealthPicked(HealPickedEvent @event)
        {
            _health.Add(@event.amount);
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
