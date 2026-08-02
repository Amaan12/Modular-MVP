using System;
using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class PresenterTwoWay<T> : IInitializable<IStat<T>, ITwoWayView<T>>, IDisposable
    {
        private IStat<T> model;
        private ITwoWayView<T> view;

        public void Init(IStat<T> model, ITwoWayView<T> view)
        {
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
