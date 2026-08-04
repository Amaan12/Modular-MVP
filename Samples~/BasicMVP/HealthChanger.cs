using UnityEngine;

namespace DesignPatterns.UI.MVP.Sample
{
    /// <summary>
    /// Sample component that dynamically changes the Health value over time to demonstrate one-way MVP binding.
    /// </summary>
    public class HealthChanger : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private float changeSpeed = 25f;

        private bool isDecreasing = true;

        private void Awake()
        {
            if (healthComponent == null)
            {
                healthComponent = GetComponent<Health>();
            }
        }

        private void Update()
        {
            if (healthComponent == null) return;

            StatRange currentRange = healthComponent.Value;
            float current = currentRange.Current;
            float max = currentRange.Max;
            float min = currentRange.Min;

            if (isDecreasing)
            {
                current -= changeSpeed * Time.deltaTime;
                if (current <= min)
                {
                    current = min;
                    isDecreasing = false;
                }
            }
            else
            {
                current += changeSpeed * Time.deltaTime;
                if (current >= max)
                {
                    current = max;
                    isDecreasing = true;
                }
            }

            healthComponent.Set(new StatRange(current, max, min));
        }
    }
}
