using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Abstract MonoBehaviour binder that synchronizes a read-only model stat to a 1-way view component.
    /// </summary>
    /// <typeparam name="T">The bound data type.</typeparam>
    public abstract class UIBinderOneWay<T> : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour modelComponent;
        [SerializeField] private MonoBehaviour viewComponent;

        private IReadOnlyStat<T> model;
        private IView<T> view;

        private void Awake()
        {
            model = modelComponent as IReadOnlyStat<T>;
            view = viewComponent as IView<T>;
        }

        private void OnEnable()
        {
            if (model != null && view != null)
            {
                model.OnChanged += view.Render;
                view.Render(model.Value);
            }
        }

        private void OnDisable()
        {
            if (model != null && view != null)
            {
                model.OnChanged -= view.Render;
            }
        }
    }
}
