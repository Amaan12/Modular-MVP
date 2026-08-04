using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Base class for DOTween-animated Slider Views.
    /// Implements ITwoWayView of float and StatRange with DOTween value tweening and customizable panel change effects.
    /// </summary>
    public abstract class BaseDOTweenSliderView : MonoBehaviour, ITwoWayView<float>, ITwoWayView<StatRange>
    {
        public event Action<float> OnUserInteracted;

        event Action<StatRange> ITwoWayView<StatRange>.OnUserInteracted
        {
            add { }
            remove { }
        }

        [Header("Components")]
        [SerializeField] protected Slider targetSlider;
        [SerializeField] protected RectTransform animatedPanel;

        [Header("DOTween Value Settings")]
        [SerializeField] protected float valueTweenDuration = 0.35f;
        [SerializeField] protected Ease valueTweenEase = Ease.OutQuad;

        protected Tweener valueTween;
        protected Vector3 initialPanelScale = Vector3.one;
        protected Vector3 initialPanelRotation = Vector3.zero;
        protected Vector3 initialPanelPosition = Vector3.zero;
        protected Vector2 initialAnchoredPosition = Vector2.zero;

        protected virtual void Awake()
        {
            if (targetSlider == null) targetSlider = GetComponent<Slider>();
            if (animatedPanel == null) animatedPanel = GetComponent<RectTransform>();

            if (animatedPanel != null)
            {
                initialPanelScale = animatedPanel.localScale;
                initialPanelRotation = animatedPanel.localEulerAngles;
                initialPanelPosition = animatedPanel.localPosition;
                initialAnchoredPosition = animatedPanel.anchoredPosition;
            }

            if (targetSlider != null)
            {
                targetSlider.onValueChanged.AddListener(OnSliderValueChanged);
            }
        }

        protected virtual void OnDestroy()
        {
            if (targetSlider != null)
            {
                targetSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
            valueTween?.Kill();
            animatedPanel?.DOKill();
        }

        protected bool hasLastValue;
        protected float lastTargetValue;

        public virtual void Render(float value)
        {
            if (hasLastValue && Mathf.Approximately(lastTargetValue, value)) return;
            hasLastValue = true;
            lastTargetValue = value;

            AnimateSliderValue(value);
            PlayEffect();
        }

        public virtual void Render(StatRange range)
        {
            float targetNorm = range.Normalized;
            if (hasLastValue && Mathf.Approximately(lastTargetValue, targetNorm)) return;
            hasLastValue = true;
            lastTargetValue = targetNorm;

            AnimateSliderValue(targetNorm);
            PlayEffect();
        }

        protected virtual void AnimateSliderValue(float targetValue)
        {
            if (targetSlider == null) return;

            valueTween?.Kill();
            valueTween = targetSlider.DOValue(targetValue, valueTweenDuration)
                .SetEase(valueTweenEase);
        }

        protected abstract void PlayEffect();

        private void OnSliderValueChanged(float newValue)
        {
            OnUserInteracted?.Invoke(newValue);
        }
    }
}
