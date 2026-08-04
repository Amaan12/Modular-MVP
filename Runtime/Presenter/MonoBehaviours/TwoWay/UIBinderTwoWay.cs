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
            if (modelComponent == null || viewComponent == null)
            {
                Debug.LogError("No Model or View Components Assigned in the Inspector", this);
                return;
            }

            model = modelComponent as IStat<T>;
            view = viewComponent as ITwoWayView<T>;

            if (model == null)
            {
                Debug.LogError($"Model should implement IStat<{typeof(T).Name}>", this);
                return;
            }
            else if (view == null)
            {
                Debug.LogError($"View should implement ITwoWayView<{typeof(T).Name}>", this);
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
                Debug.LogError($"Model should implement IStat<{typeof(T).Name}>", this);
                return;
            }
            else if (view == null)
            {
                Debug.LogError($"View should implement ITwoWayView<{typeof(T).Name}>", this);
                return;
            }

            model.OnChanged += view.Render;
            view.OnUserInteracted += model.Set;
            view.Render(model.Value);
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
