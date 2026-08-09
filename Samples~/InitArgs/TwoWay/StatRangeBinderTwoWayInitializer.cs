using Sisus.Init;
using DesignPatterns.UI.MVP;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for StatRangeBinderTwoWay.
    /// </summary>
    public class StatRangeBinderTwoWayWrapper : Wrapper<BinderTwoWay<StatRange>> { }

    /// <summary>
    /// InitArgs wrapper initializer for StatRangeBinderTwoWay.
    /// </summary>
    public class StatRangeBinderTwoWayInitializer : WrapperInitializer<StatRangeBinderTwoWayWrapper, BinderTwoWay<StatRange>, IStat<StatRange>, ITwoWayView<StatRange>>
    {
        protected override BinderTwoWay<StatRange> CreateWrappedObject(IStat<StatRange> model, ITwoWayView<StatRange> view)
            => new BinderTwoWay<StatRange>(model, view);
    }
}
