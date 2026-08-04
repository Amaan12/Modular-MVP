using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// View component that renders and handles user interactions for UnityEngine.UI.Slider components.
    /// </summary>
    public class SliderView : MonoBehaviour, ITwoWayView<float>, ITwoWayView<StatRange>
    {
        public event Action<float> OnUserInteracted;

        event Action<StatRange> ITwoWayView<StatRange>.OnUserInteracted
        {
            add { }
            remove { }
        }

        [SerializeField] private Slider slider;

        private void Awake()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }

            if (slider == null)
            {
                Debug.LogError("No Slider Component Assigned in the Inspector", this);
                return;
            }

            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnValidate()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
        }

        private void Reset()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
        }

        private void OnDestroy()
        {
            if (slider != null)
            {
                slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
        }

        public void Render(float value)
        {
            if (slider == null)
            {
                Debug.LogError("No Slider Component Assigned in the Inspector", this);
                return;
            }

            slider.SetValueWithoutNotify(value);
        }

        public void Render(StatRange range)
        {
            if (slider == null)
            {
                Debug.LogError("No Slider Component Assigned in the Inspector", this);
                return;
            }

            slider.SetValueWithoutNotify(range.Normalized);
        }

        private void OnSliderValueChanged(float newValue)
        {
            OnUserInteracted?.Invoke(newValue);
        }
    }
}
