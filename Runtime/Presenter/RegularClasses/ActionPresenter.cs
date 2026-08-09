using System;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Generic plain C# presenter for 1-way event binding without requiring IStat.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public class ActionPresenter<T> : IDisposable
    {
        private readonly Action<Action<T>> _unsubscribe;
        private readonly Action<T> _onValueChanged;

        public ActionPresenter(
            T initialValue,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe,
            IView<T> view)
        {
            if (view == null)
            {
                UnityEngine.Debug.LogError($"View should implement IView<{typeof(T).Name}>");
                return;
            }
            if (subscribe == null)
            {
                UnityEngine.Debug.LogError("Subscribe action cannot be null");
                return;
            }

            _unsubscribe = unsubscribe;
            _onValueChanged = view.Render;

            subscribe(_onValueChanged);
            view.Render(initialValue);
        }

        public void Dispose()
        {
            _unsubscribe?.Invoke(_onValueChanged);
        }
    }
}
