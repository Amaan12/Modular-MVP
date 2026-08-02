using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class IntPresenterTwoWayWrapper : Wrapper<PresenterTwoWay<int>> { }

    public class IntPresenterTwoWayInitializer : WrapperInitializer<IntPresenterTwoWayWrapper, PresenterTwoWay<int>, IStat<int>, ITwoWayView<int>>
    {
        protected override PresenterTwoWay<int> CreateWrappedObject(IStat<int> model, ITwoWayView<int> view)
            => new PresenterTwoWay<int>();
    }
}
