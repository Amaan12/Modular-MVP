using DG.Tweening;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Text view that punches text scale on value changes.
    /// </summary>
    public class PunchTextView : BaseDOTweenTextView
    {
        [Header("Punch Scale Effect")]
        [SerializeField] private Vector3 punchScale = new Vector3(0.3f, 0.3f, 0f);
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private int vibrato = 8;
        [SerializeField] private float elasticity = 1f;

        protected override void PlayEffect()
        {
            if (animatedTransform == null) return;
            animatedTransform.DOComplete();
            animatedTransform.DOPunchScale(punchScale, duration, vibrato, elasticity);
        }
    }
}
