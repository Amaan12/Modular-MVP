using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Slider view that rotates/tilts the panel left and right on value changes.
    /// </summary>
    public class RotateSliderView : BaseDOTweenSliderView
    {
        [Header("Rotate Left & Right Effect")]
        [SerializeField] private Vector3 punchRotation = new Vector3(0f, 0f, 15f);
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private int vibrato = 8;
        [SerializeField] private float elasticity = 1f;

        protected override void PlayEffect()
        {
            if (animatedPanel == null) return;
            animatedPanel.DOComplete();
            animatedPanel.DOPunchRotation(punchRotation, duration, vibrato, elasticity);
        }
    }
}
