using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Slider view that applies a Punch Scale effect to the panel on value changes.
    /// </summary>
    public class PunchSliderView : BaseDOTweenSliderView
    {
        [Header("Punch Scale Effect")]
        [SerializeField] private Vector3 punchAmount = new Vector3(0.15f, 0.15f, 0f);
        [SerializeField] private float punchDuration = 0.3f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1f;

        protected override void PlayEffect()
        {
            if (animatedPanel == null) return;
            animatedPanel.DOComplete();
            animatedPanel.DOPunchScale(punchAmount, punchDuration, vibrato, elasticity);
        }
    }
}
