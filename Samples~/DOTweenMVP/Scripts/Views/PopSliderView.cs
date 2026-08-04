using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Slider view that applies a Pop / Bounce Scale sequence to the panel on value changes.
    /// </summary>
    public class PopSliderView : BaseDOTweenSliderView
    {
        [Header("Pop / Bounce Scale Effect")]
        [SerializeField] private float scaleFactor = 1.15f;
        [SerializeField] private float popDuration = 0.15f;
        [SerializeField] private float returnDuration = 0.2f;

        protected override void PlayEffect()
        {
            if (animatedPanel == null) return;
            animatedPanel.DOComplete();
            
            Sequence seq = DOTween.Sequence();
            seq.Append(animatedPanel.DOScale(initialPanelScale * scaleFactor, popDuration).SetEase(Ease.OutQuad));
            seq.Append(animatedPanel.DOScale(initialPanelScale, returnDuration).SetEase(Ease.OutBack));
        }
    }
}
