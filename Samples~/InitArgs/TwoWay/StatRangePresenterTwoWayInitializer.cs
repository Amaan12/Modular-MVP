using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class StatRangePresenterTwoWayWrapper : Wrapper<PresenterTwoWay<StatRange>> { }

    public class StatRangePresenterTwoWayInitializer : WrapperInitializer<StatRangePresenterTwoWayWrapper, PresenterTwoWay<StatRange>, IStat<StatRange>, ITwoWayView<StatRange>>
    {
        protected override PresenterTwoWay<StatRange> CreateWrappedObject(IStat<StatRange> model, ITwoWayView<StatRange> view)
            => new PresenterTwoWay<StatRange>();
    }
}
