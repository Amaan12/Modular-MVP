using System;
using UnityEngine;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Generic MonoBehaviour stat component implementing IStat of StatRange for RPG character stats.
    /// </summary>
    public class CharacterStat : MonoBehaviour, IStat<StatRange>
    {
        public event Action<StatRange> OnChanged;

        [SerializeField] private string statName = "Stat";
        [SerializeField] private StatRange stat = new StatRange(100f, 100f, 0f);

        public string StatName => statName;
        public StatRange Value => stat;

        public void Set(StatRange newRange)
        {
            stat = new StatRange(newRange.Current, newRange.Max, newRange.Min);
            OnChanged?.Invoke(stat);
        }

        public void Reduce(float amount)
        {
            Set(new StatRange(stat.Current - amount, stat.Max, stat.Min));
        }

        public void Restore(float amount)
        {
            Set(new StatRange(stat.Current + amount, stat.Max, stat.Min));
        }
    }
}
