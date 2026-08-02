using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class StringPresenterTwoWayWrapper : Wrapper<PresenterTwoWay<string>> { }

    public class StringPresenterTwoWayInitializer : WrapperInitializer<StringPresenterTwoWayWrapper, PresenterTwoWay<string>, IStat<string>, ITwoWayView<string>>
    {
        protected override PresenterTwoWay<string> CreateWrappedObject(IStat<string> model, ITwoWayView<string> view)
            => new PresenterTwoWay<string>();
    }
}
