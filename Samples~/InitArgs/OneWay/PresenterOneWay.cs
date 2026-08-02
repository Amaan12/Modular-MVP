using System;
using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class PresenterOneWay<T> : IInitializable<IReadOnlyStat<T>, IView<T>>, IDisposable
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
