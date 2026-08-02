using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    public abstract class UIBinderOneWay<T> : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour modelObject;
        [SerializeField] private MonoBehaviour viewObject;

        private IReadOnlyStat<T> model;
        private IView<T> view;

        protected virtual void Awake()
        {
            model = modelObject as IReadOnlyStat<T>;
            view = viewObject as IView<T>;
        }

        protected virtual void OnEnable()
        {
            if (model == null || view == null) return;

            model.OnChanged += view.Render;
            view.Render(model.Value);
        }

        protected virtual void OnDisable()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
            }
        }
    }
}
