namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Read-write stat contract inheriting read-only and mutable stat contracts.
    /// </summary>
    /// <typeparam name="T">The stat value type.</typeparam>
    public interface IStat<T> : IReadOnlyStat<T>, IMutableStat<T>
    {
    }
}
