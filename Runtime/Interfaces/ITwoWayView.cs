using System;

namespace DesignPatterns.UI.MVP
{
    public interface ITwoWayView<T> : IView<T>
    {
        event Action<T> OnUserInteracted;
    }
}
