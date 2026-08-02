using System;

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

        public float Normalized => Max > 0f ? Current / Max : 0f;

        public StatRange(float current, float max)
        {
            Current = current;
            Max = max;
        }
    }
}
