using System;
using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// Plain C# 1-way binder implementing InitArgs IInitializable and IDisposable.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public class BinderOneWay<T> : IInitializable<IReadOnlyStat<T>, IView<T>>, IDisposable
    {
        private IReadOnlyStat<T> model;
        private IView<T> view;

        public void Init(IReadOnlyStat<T> model, IView<T> view)
        {
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
