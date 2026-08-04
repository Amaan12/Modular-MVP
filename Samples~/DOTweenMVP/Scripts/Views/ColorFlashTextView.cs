using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Text view that flashes text color on value changes.
    /// </summary>
    public class ColorFlashTextView : BaseDOTweenTextView
    {
        [Header("Color Flash Effect")]
        [SerializeField] private Color flashColor = Color.yellow;
        [SerializeField] private float flashDuration = 0.15f;
        [SerializeField] private float returnDuration = 0.2f;

        protected override void PlayEffect()
        {
            if (textComponent == null) return;
            textComponent.DOComplete();

            Sequence seq = DOTween.Sequence();
            seq.Append(textComponent.DOColor(flashColor, flashDuration));
            seq.Append(textComponent.DOColor(initialColor, returnDuration));
        }
    }
}
