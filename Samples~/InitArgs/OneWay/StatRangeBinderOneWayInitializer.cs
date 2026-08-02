using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for StatRangeBinderOneWay.
    /// </summary>
    public class StatRangeBinderOneWayWrapper : Wrapper<BinderOneWay<StatRange>> { }

    /// <summary>
    /// InitArgs wrapper initializer for StatRangeBinderOneWay.
    /// </summary>
    public class StatRangeBinderOneWayInitializer : WrapperInitializer<StatRangeBinderOneWayWrapper, BinderOneWay<StatRange>, IReadOnlyStat<StatRange>, IView<StatRange>>
    {
        protected override BinderOneWay<StatRange> CreateWrappedObject(IReadOnlyStat<StatRange> model, IView<StatRange> view)
            => new BinderOneWay<StatRange>();
    }
}
