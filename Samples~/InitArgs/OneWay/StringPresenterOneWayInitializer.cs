using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class StringPresenterOneWayWrapper : Wrapper<PresenterOneWay<string>> { }

    public class StringPresenterOneWayInitializer : WrapperInitializer<StringPresenterOneWayWrapper, PresenterOneWay<string>, IReadOnlyStat<string>, IView<string>>
    {
        protected override PresenterOneWay<string> CreateWrappedObject(IReadOnlyStat<string> model, IView<string> view)
            => new PresenterOneWay<string>();
    }
}
