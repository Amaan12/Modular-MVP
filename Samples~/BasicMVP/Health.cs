using System;
using UnityEngine;

namespace DesignPatterns.UI.MVP.Sample
{
    public class Health : MonoBehaviour, IDamageable, IStat<StatRange>
    {
        public event Action<StatRange> OnChanged;

        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;

        public StatRange Value => new StatRange(currentHealth, maxHealth);

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void Set(StatRange newRange)
        {
            currentHealth = Mathf.Clamp(newRange.Current, 0f, newRange.Max);
            maxHealth = newRange.Max;
            OnChanged?.Invoke(Value);
        }

        public void TakeDamage(float amount)
        {
            Set(new StatRange(currentHealth - amount, maxHealth));
        }

        public void Heal(float amount)
        {
            Set(new StatRange(currentHealth + amount, maxHealth));
        }
    }
}
