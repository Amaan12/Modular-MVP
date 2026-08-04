using TMPro;
using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Base class for DOTween-animated Text Views.
    /// Implements IView of string, int, float, and StatRange with smooth number value tweening and customizable text effects.
    /// </summary>
    public abstract class BaseDOTweenTextView : MonoBehaviour, IView<string>, IView<int>, IView<float>, IView<StatRange>
    {
        [Header("Components")]
        [SerializeField] protected TextMeshProUGUI textComponent;
        [SerializeField] protected RectTransform animatedTransform;

        [Header("DOTween Value Settings")]
        [SerializeField] protected float numberTweenDuration = 0.35f;
        [SerializeField] protected Ease numberTweenEase = Ease.OutQuad;

        protected float currentDisplayedValue;
        protected float targetDisplayedMax;
        protected bool isStatRangeMode;
        protected Tweener numberTween;

        protected Vector3 initialScale = Vector3.one;
        protected Vector3 initialRotation = Vector3.zero;
        protected Vector3 initialPosition = Vector3.zero;
        protected Vector2 initialAnchoredPosition = Vector2.zero;
        protected Color initialColor = Color.white;

        protected virtual void Awake()
        {
            if (textComponent == null) textComponent = GetComponent<TextMeshProUGUI>();
            if (animatedTransform == null) animatedTransform = GetComponent<RectTransform>();

            if (animatedTransform != null)
            {
                initialScale = animatedTransform.localScale;
                initialRotation = animatedTransform.localEulerAngles;
                initialPosition = animatedTransform.localPosition;
                initialAnchoredPosition = animatedTransform.anchoredPosition;
            }

            if (textComponent != null)
            {
                initialColor = textComponent.color;
            }
        }

        protected virtual void OnDestroy()
        {
            numberTween?.Kill();
            animatedTransform?.DOKill();
            if (textComponent != null) textComponent.DOKill();
        }

        protected bool hasLastValue;
        protected string lastRenderedString;
        protected float lastTargetVal;
        protected float lastTargetMax;

        public virtual void Render(string text)
        {
            if (hasLastValue && lastRenderedString == text) return;
            hasLastValue = true;
            lastRenderedString = text;

            numberTween?.Kill();
            SetTextDirect(text);
            PlayEffect();
        }

        public virtual void Render(int value)
        {
            if (hasLastValue && !isStatRangeMode && Mathf.Approximately(lastTargetVal, value)) return;
            hasLastValue = true;
            isStatRangeMode = false;
            lastTargetVal = value;

            AnimateNumberTo(value, isRange: false);
            PlayEffect();
        }

        public virtual void Render(float value)
        {
            if (hasLastValue && !isStatRangeMode && Mathf.Approximately(lastTargetVal, value)) return;
            hasLastValue = true;
            isStatRangeMode = false;
            lastTargetVal = value;

            AnimateNumberTo(value, isRange: false);
            PlayEffect();
        }

        public virtual void Render(StatRange range)
        {
            if (hasLastValue && isStatRangeMode && Mathf.Approximately(lastTargetVal, range.Current) && Mathf.Approximately(lastTargetMax, range.Max)) return;
            hasLastValue = true;
            isStatRangeMode = true;
            lastTargetVal = range.Current;
            lastTargetMax = range.Max;

            targetDisplayedMax = range.Max;
            AnimateNumberTo(range.Current, isRange: true);
            PlayEffect();
        }

        protected virtual void AnimateNumberTo(float targetVal, bool isRange)
        {
            if (textComponent == null) return;

            isStatRangeMode = isRange;
            numberTween?.Kill();

            numberTween = DOTween.To(() => currentDisplayedValue, x =>
            {
                currentDisplayedValue = x;
                UpdateTextDisplay();
            }, targetVal, numberTweenDuration).SetEase(numberTweenEase);
        }

        protected virtual void UpdateTextDisplay()
        {
            if (textComponent == null) return;

            if (isStatRangeMode)
            {
                textComponent.text = $"{Mathf.CeilToInt(currentDisplayedValue)} / {Mathf.CeilToInt(targetDisplayedMax)}";
            }
            else
            {
                textComponent.text = Mathf.CeilToInt(currentDisplayedValue).ToString();
            }
        }

        protected virtual void SetTextDirect(string content)
        {
            if (textComponent != null)
            {
                textComponent.text = content;
            }
        }

        protected abstract void PlayEffect();
    }
}
