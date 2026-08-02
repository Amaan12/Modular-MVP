using System;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Read-only contract for UI display exposing value change notifications.
    /// </summary>
    /// <typeparam name="T">The stat value type.</typeparam>
    public interface IReadOnlyStat<T>
    {
        event Action<T> OnChanged;
        T Value { get; }
    }
}
