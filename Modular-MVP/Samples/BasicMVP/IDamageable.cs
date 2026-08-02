namespace DesignPatterns.UI.MVP.Sample
{
    public interface IDamageable : IReadOnlyStat<StatRange>
    {
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}
