using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class IntPresenterOneWayWrapper : Wrapper<PresenterOneWay<int>> { }

    public class IntPresenterOneWayInitializer : WrapperInitializer<IntPresenterOneWayWrapper, PresenterOneWay<int>, IReadOnlyStat<int>, IView<int>>
    {
        protected override PresenterOneWay<int> CreateWrappedObject(IReadOnlyStat<int> model, IView<int> view)
            => new PresenterOneWay<int>();
    }
}
