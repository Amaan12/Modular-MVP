using System;
using UnityEngine;

namespace DesignPatterns.UI.MVP.Sample
{
    /// <summary>
    /// Sample Health component implementing IDamageable and IStat of StatRange.
    /// </summary>
    public class Health : MonoBehaviour, IDamageable, IStat<StatRange>
    {
        public event Action<StatRange> OnChanged;

        [SerializeField] private StatRange health = new StatRange(100f, 100f);

        public StatRange Value => health;

        public void Set(StatRange newRange)
        {
            health = new StatRange(newRange.Current, newRange.Max, newRange.Min);
            OnChanged?.Invoke(health);
        }

        public void TakeDamage(float amount)
        {
            Set(new StatRange(health.Current - amount, health.Max, health.Min));
        }

        public void Heal(float amount)
        {
            Set(new StatRange(health.Current + amount, health.Max, health.Min));
        }
    }
}
