using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class FloatPresenterTwoWayWrapper : Wrapper<PresenterTwoWay<float>> { }

    public class FloatPresenterTwoWayInitializer : WrapperInitializer<FloatPresenterTwoWayWrapper, PresenterTwoWay<float>, IStat<float>, ITwoWayView<float>>
    {
        protected override PresenterTwoWay<float> CreateWrappedObject(IStat<float> model, ITwoWayView<float> view)
            => new PresenterTwoWay<float>();
    }
}
