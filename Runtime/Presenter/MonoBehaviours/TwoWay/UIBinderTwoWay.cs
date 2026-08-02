using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Abstract MonoBehaviour binder that synchronizes a read-write stat model with a 2-way interactive view component.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public abstract class UIBinderTwoWay<T> : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour modelComponent;
        [SerializeField] private MonoBehaviour viewComponent;

        private IStat<T> model;
        private ITwoWayView<T> view;

        private void Awake()
        {
            model = modelComponent as IStat<T>;
            view = viewComponent as ITwoWayView<T>;
        }

        private void OnEnable()
        {
            if (model != null && view != null)
            {
                model.OnChanged += view.Render;
                view.OnUserInteracted += model.Set;
                view.Render(model.Value);
            }
        }

        private void OnDisable()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
                view.OnUserInteracted -= model.Set;
            }
        }
    }
}
