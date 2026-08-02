namespace DesignPatterns.UI.MVP
{
    public interface IStat<T> : IReadOnlyStat<T>, IMutableStat<T>
    {
    }
}
