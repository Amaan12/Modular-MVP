using System;

namespace DesignPatterns.UI.MVP
{
    public interface IReadOnlyStat<T>
    {
        event Action<T> OnChanged;
        T Value { get; }
    }
}
