using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Slider view that applies a Shake Position effect to the panel on value changes.
    /// </summary>
    public class ShakeSliderView : BaseDOTweenSliderView
    {
        [Header("Shake Position Effect")]
        [SerializeField] private float shakeDuration = 0.35f;
        [SerializeField] private Vector2 shakeStrength = new Vector2(8f, 4f);
        [SerializeField] private int vibrato = 12;
        [SerializeField] private float randomness = 90f;

        protected override void PlayEffect()
        {
            if (animatedPanel == null) return;
            animatedPanel.DOComplete();
            animatedPanel.DOShakeAnchorPos(shakeDuration, shakeStrength, vibrato, randomness);
        }
    }
}
