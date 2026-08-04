using System;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// Plain C# 1-way binder using constructor injection and implementing IDisposable.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public class BinderOneWay<T> : IDisposable
    {
        private readonly IReadOnlyStat<T> model;
        private readonly IView<T> view;

        public BinderOneWay(IReadOnlyStat<T> model, IView<T> view)
        {
            if (model == null)
            {
                UnityEngine.Debug.LogError($"Model should implement IReadOnlyStat<{typeof(T).Name}>");
                return;
            }
            else if (view == null)
            {
                UnityEngine.Debug.LogError($"View should implement IView<{typeof(T).Name}>");
                return;
            }

            this.model = model;
            this.view = view;

            this.model.OnChanged += this.view.Render;
            this.view.Render(this.model.Value);
        }

        public void Dispose()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
            }
        }
    }
}
