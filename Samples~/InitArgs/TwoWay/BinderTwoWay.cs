using System;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// Plain C# 2-way binder using constructor injection and implementing IDisposable.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public class BinderTwoWay<T> : IDisposable
    {
        private readonly IStat<T> model;
        private readonly ITwoWayView<T> view;

        public BinderTwoWay(IStat<T> model, ITwoWayView<T> view)
        {
            if (model == null)
            {
                UnityEngine.Debug.LogError($"Model should implement IStat<{typeof(T).Name}>");
                return;
            }
            else if (view == null)
            {
                UnityEngine.Debug.LogError($"View should implement ITwoWayView<{typeof(T).Name}>");
                return;
            }

            this.model = model;
            this.view = view;

            this.model.OnChanged += this.view.Render;
            this.view.OnUserInteracted += this.model.Set;

            this.view.Render(this.model.Value);
        }

        public void Dispose()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
                view.OnUserInteracted -= model.Set;
            }
        }
    }
}
