using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Text view that shakes text position on value changes.
    /// </summary>
    public class ShakeTextView : BaseDOTweenTextView
    {
        [Header("Shake Position Effect")]
        [SerializeField] private float duration = 0.35f;
        [SerializeField] private Vector2 shakeStrength = new Vector2(6f, 6f);
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;

        protected override void PlayEffect()
        {
            if (animatedTransform == null) return;
            animatedTransform.DOComplete();
            animatedTransform.DOShakeAnchorPos(duration, shakeStrength, vibrato, randomness);
        }
    }
}
