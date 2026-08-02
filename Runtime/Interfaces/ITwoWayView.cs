using System;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Two-way interactive UI view contract exposing user interaction events.
    /// </summary>
    /// <typeparam name="T">The view data type.</typeparam>
    public interface ITwoWayView<T> : IView<T>
    {
        event Action<T> OnUserInteracted;
    }
}
