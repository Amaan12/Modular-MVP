using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for StringBinderOneWay.
    /// </summary>
    public class StringBinderOneWayWrapper : Wrapper<BinderOneWay<string>> { }

    /// <summary>
    /// InitArgs wrapper initializer for StringBinderOneWay.
    /// </summary>
    public class StringBinderOneWayInitializer : WrapperInitializer<StringBinderOneWayWrapper, BinderOneWay<string>, IReadOnlyStat<string>, IView<string>>
    {
        protected override BinderOneWay<string> CreateWrappedObject(IReadOnlyStat<string> model, IView<string> view)
            => new BinderOneWay<string>();
    }
}
