using System;
using UnityEngine;

namespace Assets._Project._scripts.Player
{
    public class Health
    {
        public event Action<float, float> Changed;

        public float Max { get; private set; }
        public float Current { get; private set; }

        public Health(float max, float current)
        {
            Max = max;
            Current = current;
        }

        public void Reduce(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            float oldValue = Current;
            Current = Mathf.Clamp(Current - value, 0, Max);

            Changed?.Invoke(oldValue, Current);
        }

        public void Add(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            float oldValue = Current;
            Current = Mathf.Clamp(Current + value, 0, Max);

            Changed?.Invoke(oldValue, Current);
        }
    }
}
