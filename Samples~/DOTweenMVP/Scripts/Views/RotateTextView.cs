using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Text view that rotates text left and right on value changes.
    /// </summary>
    public class RotateTextView : BaseDOTweenTextView
    {
        [Header("Rotate Left & Right Effect")]
        [SerializeField] private Vector3 punchRotation = new Vector3(0f, 0f, 20f);
        [SerializeField] private float duration = 0.35f;
        [SerializeField] private int vibrato = 8;
        [SerializeField] private float elasticity = 1f;

        protected override void PlayEffect()
        {
            if (animatedTransform == null) return;
            animatedTransform.DOComplete();
            animatedTransform.DOPunchRotation(punchRotation, duration, vibrato, elasticity);
        }
    }
}
