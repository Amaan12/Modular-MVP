using UnityEngine;
using UnityEngine.InputSystem;

namespace DesignPatterns.UI.MVP.DOTweenSample
{
    /// <summary>
    /// Stat changer for RPG character stats.
    /// Handles keypress reduction (Keys 1-4) with cooldown reset, and discrete auto-regeneration (no smoothing).
    /// </summary>
    public class RPGStatChanger : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private CharacterStat healthStat;
        [SerializeField] private CharacterStat manaStat;
        [SerializeField] private CharacterStat staminaStat;
        [SerializeField] private CharacterStat shieldStat;

        [Header("Settings")]
        [SerializeField] private float damageAmount = 20f;
        [SerializeField] private float autoRegenAmount = 5f;
        [SerializeField] private float autoRegenInterval = 1f;

        private float healthTimer;
        private float manaTimer;
        private float staminaTimer;
        private float shieldTimer;

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.digit1Key.wasPressedThisFrame && healthStat != null)
                {
                    healthStat.Reduce(damageAmount);
                    healthTimer = 0f;
                }

                if (keyboard.digit2Key.wasPressedThisFrame && manaStat != null)
                {
                    manaStat.Reduce(damageAmount);
                    manaTimer = 0f;
                }

                if (keyboard.digit3Key.wasPressedThisFrame && staminaStat != null)
                {
                    staminaStat.Reduce(damageAmount);
                    staminaTimer = 0f;
                }

                if (keyboard.digit4Key.wasPressedThisFrame && shieldStat != null)
                {
                    shieldStat.Reduce(damageAmount);
                    shieldTimer = 0f;
                }
            }

            // Health Regen (discrete tick)
            if (healthStat != null)
            {
                healthTimer += Time.deltaTime;
                if (healthTimer >= autoRegenInterval)
                {
                    healthTimer -= autoRegenInterval;
                    healthStat.Restore(autoRegenAmount);
                }
            }

            // Mana Regen (discrete tick)
            if (manaStat != null)
            {
                manaTimer += Time.deltaTime;
                if (manaTimer >= autoRegenInterval)
                {
                    manaTimer -= autoRegenInterval;
                    manaStat.Restore(autoRegenAmount);
                }
            }

            // Stamina Regen (discrete tick)
            if (staminaStat != null)
            {
                staminaTimer += Time.deltaTime;
                if (staminaTimer >= autoRegenInterval)
                {
                    staminaTimer -= autoRegenInterval;
                    staminaStat.Restore(autoRegenAmount);
                }
            }

            // Shield Regen (discrete tick)
            if (shieldStat != null)
            {
                shieldTimer += Time.deltaTime;
                if (shieldTimer >= autoRegenInterval)
                {
                    shieldTimer -= autoRegenInterval;
                    shieldStat.Restore(autoRegenAmount);
                }
            }
        }
    }
}
