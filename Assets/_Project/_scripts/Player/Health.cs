using Assets._Project._scripts._core.Reactive;
using System;
using UnityEngine;

namespace Assets._Project._scripts.Player
{
    public class Health
    {
        public ReactiveVariable<float> Max { get; }
        public ReactiveVariable<float> Current { get; }

        public Health(float max, float current)
        {
            Max = new ReactiveVariable<float>(max);
            Current = new ReactiveVariable<float>(current);
        }

        public void Reduce(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            float oldValue = Current._value;
            Current._value = Mathf.Clamp(Current._value - value, 0, Max._value);
        }

        public void Add(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            float oldValue = Current._value;
            Current._value = Mathf.Clamp(Current._value + value, 0, Max._value);
        }
    }
}
