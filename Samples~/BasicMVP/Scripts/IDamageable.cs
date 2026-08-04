namespace DesignPatterns.UI.MVP.Sample
{
    /// <summary>
    /// Sample domain interface for damageable entities exposing read-only stat events and damage/heal operations.
    /// </summary>
    public interface IDamageable : IReadOnlyStat<StatRange>
    {
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}
