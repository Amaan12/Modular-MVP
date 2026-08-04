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
            if (modelComponent == null || viewComponent == null)
            {
                Debug.LogError("No Model or View Components Assigned in the Inspector", this);
                return;
            }

            model = modelComponent as IReadOnlyStat<T>;
            view = viewComponent as IView<T>;

            if (model == null)
            {
                Debug.LogError($"Model should implement IReadOnlyStat<{typeof(T).Name}>", this);
                return;
            }
            else if (view == null)
            {
                Debug.LogError($"View should implement IView<{typeof(T).Name}>", this);
                return;
            }
        }

        private void OnEnable()
        {
            if (modelComponent == null || viewComponent == null)
            {
                Debug.LogError("No Model or View Components Assigned in the Inspector", this);
                return;
            }

            if (model == null)
            {
                Debug.LogError($"Model should implement IReadOnlyStat<{typeof(T).Name}>", this);
                return;
            }
            else if (view == null)
            {
                Debug.LogError($"View should implement IView<{typeof(T).Name}>", this);
                return;
            }

            model.OnChanged += view.Render;
            view.Render(model.Value);
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
