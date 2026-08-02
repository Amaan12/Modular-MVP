using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class StatRangePresenterOneWayWrapper : Wrapper<PresenterOneWay<StatRange>> { }

    public class StatRangePresenterOneWayInitializer : WrapperInitializer<StatRangePresenterOneWayWrapper, PresenterOneWay<StatRange>, IReadOnlyStat<StatRange>, IView<StatRange>>
    {
        protected override PresenterOneWay<StatRange> CreateWrappedObject(IReadOnlyStat<StatRange> model, IView<StatRange> view)
            => new PresenterOneWay<StatRange>();
    }
}
