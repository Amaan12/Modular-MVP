using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for IntBinderTwoWay.
    /// </summary>
    public class IntBinderTwoWayWrapper : Wrapper<BinderTwoWay<int>> { }

    /// <summary>
    /// InitArgs wrapper initializer for IntBinderTwoWay.
    /// </summary>
    public class IntBinderTwoWayInitializer : WrapperInitializer<IntBinderTwoWayWrapper, BinderTwoWay<int>, IStat<int>, ITwoWayView<int>>
    {
        protected override BinderTwoWay<int> CreateWrappedObject(IStat<int> model, ITwoWayView<int> view)
            => new BinderTwoWay<int>(model, view);
    }
}
