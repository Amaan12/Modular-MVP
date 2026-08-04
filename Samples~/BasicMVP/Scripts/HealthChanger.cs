using UnityEngine;
using UnityEngine.InputSystem;

namespace DesignPatterns.UI.MVP.Sample
{
    /// <summary>
    /// Sample component that manages Health by applying damage on key press and periodic auto-healing.
    /// </summary>
    public class HealthChanger : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private float damageAmount = 20f;
        [SerializeField] private float autoHealAmount = 5f;
        [SerializeField] private float autoHealInterval = 1f;

        private float healTimer;

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

            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                healthComponent.TakeDamage(damageAmount);
                healTimer = 0f;
            }

            healTimer += Time.deltaTime;
            if (healTimer >= autoHealInterval)
            {
                healTimer -= autoHealInterval;
                healthComponent.Heal(autoHealAmount);
            }
        }
    }
}
