using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    public abstract class UIBinderTwoWay<T> : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour modelObject;
        [SerializeField] private MonoBehaviour viewObject;

        private IStat<T> model;
        private ITwoWayView<T> view;

        protected virtual void Awake()
        {
            model = modelObject as IStat<T>;
            view = viewObject as ITwoWayView<T>;
        }

        protected virtual void OnEnable()
        {
            if (model == null || view == null) return;

            model.OnChanged += view.Render;
            view.OnUserInteracted += model.Set;

            view.Render(model.Value);
        }

        protected virtual void OnDisable()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
                view.OnUserInteracted -= model.Set;
            }
        }
    }
}
