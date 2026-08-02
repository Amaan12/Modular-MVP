using System;
using UnityEngine;

namespace DesignPatterns.UI.MVP.Sample
{
    public class Health : MonoBehaviour, IDamageable, IStat<StatRange>
    {
        public event Action<StatRange> OnChanged;

        [SerializeField] private StatRange health = new StatRange(100f, 100f);

        public StatRange Value => health;

        public void Set(StatRange newRange)
        {
            health = new StatRange(Mathf.Clamp(newRange.Current, 0f, newRange.Max), newRange.Max);
            OnChanged?.Invoke(health);
        }

        public void TakeDamage(float amount)
        {
            Set(new StatRange(health.Current - amount, health.Max));
        }

        public void Heal(float amount)
        {
            Set(new StatRange(health.Current + amount, health.Max));
        }
    }
}
