using System;
using System.Collections.Generic;
using DesignPatterns.UI.MVP;

namespace _Project.Scripts.Model
{
    /// <summary>
    /// Generic implementation of IStat<T> for reactive data stats.
    /// </summary>
    /// <typeparam name="T">The stat value type.</typeparam>
    public class Stat<T> : IStat<T>
    {
        private T _value;

        public event Action<T> OnChanged;

        public T Value => _value;

        public Stat(T initialValue = default)
        {
            _value = initialValue;
        }

        public void Set(T newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(_value, newValue))
            {
                _value = newValue;
                OnChanged?.Invoke(_value);
            }
        }
    }
}
