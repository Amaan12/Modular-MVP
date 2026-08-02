using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    public class FloatPresenterOneWayWrapper : Wrapper<PresenterOneWay<float>> { }

    public class FloatPresenterOneWayInitializer : WrapperInitializer<FloatPresenterOneWayWrapper, PresenterOneWay<float>, IReadOnlyStat<float>, IView<float>>
    {
        protected override PresenterOneWay<float> CreateWrappedObject(IReadOnlyStat<float> model, IView<float> view)
            => new PresenterOneWay<float>();
    }
}
