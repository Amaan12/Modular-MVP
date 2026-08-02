namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// Write-only contract for state mutation without event subscriptions.
    /// </summary>
    /// <typeparam name="T">The stat value type.</typeparam>
    public interface IMutableStat<T>
    {
        void Set(T newValue);
    }
}
