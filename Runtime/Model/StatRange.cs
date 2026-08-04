using System;
using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Lightweight serializable struct holding Current and Max values with a Normalized ratio.
    /// </summary>
    [Serializable]
    public struct StatRange
    {
        public float Current;
        public float Max;
        public float Min;

        public float Normalized => Max - Min > 0f ? (Current - Min) / (Max - Min) : 0f;

        public StatRange(float current, float max, float min = 0f)
        {
            Min = min;
            Max = max;
            Current = Mathf.Clamp(current, min, max);
        }
    }
}
