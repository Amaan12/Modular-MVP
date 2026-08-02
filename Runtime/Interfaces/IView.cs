namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// One-way rendering contract for UI visual elements.
    /// </summary>
    /// <typeparam name="T">The view data type.</typeparam>
    public interface IView<T>
    {
        void Render(T value);
    }
}
