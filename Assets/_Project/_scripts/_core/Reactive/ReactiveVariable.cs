using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._scripts._core.Reactive
{
    public class ReactiveVariable<T>
    {
        public event Action<T, T> Changed;

        private T _value;
        private IEqualityComparer<T> _comparer;

        public ReactiveVariable() : this(default(T)) 
        { 
        }

        public ReactiveVariable(T value) : this(value, EqualityComparer<T>.Default) 
        { 
        }
        
        public ReactiveVariable(T value, IEqualityComparer<T> comparer)
        {
            _value = value;
            _comparer = comparer;
        }

        public T Value
        {
            get => _value;

            set
            {
                T oldValue = _value;
                _value = value;
 
                if (_comparer.Equals(oldValue, value) == false)
                {
                    Changed?.Invoke(oldValue, _value);
                }
            }
        }
    }
}
