using Sisus.Init;
using DesignPatterns.UI.MVP;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for StringBinderTwoWay.
    /// </summary>
    public class StringBinderTwoWayWrapper : Wrapper<BinderTwoWay<string>> { }

    /// <summary>
    /// InitArgs wrapper initializer for StringBinderTwoWay.
    /// </summary>
    public class StringBinderTwoWayInitializer : WrapperInitializer<StringBinderTwoWayWrapper, BinderTwoWay<string>, IStat<string>, ITwoWayView<string>>
    {
        protected override BinderTwoWay<string> CreateWrappedObject(IStat<string> model, ITwoWayView<string> view)
            => new BinderTwoWay<string>(model, view);
    }
}
