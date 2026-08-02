namespace DesignPatterns.UI.MVP
{
    public interface IView<T>
    {
        void Render(T value);
    }
}
