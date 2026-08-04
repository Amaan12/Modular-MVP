using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for FloatBinderOneWay.
    /// </summary>
    public class FloatBinderOneWayWrapper : Wrapper<BinderOneWay<float>> { }

    /// <summary>
    /// InitArgs wrapper initializer for FloatBinderOneWay.
    /// </summary>
    public class FloatBinderOneWayInitializer : WrapperInitializer<FloatBinderOneWayWrapper, BinderOneWay<float>, IReadOnlyStat<float>, IView<float>>
    {
        protected override BinderOneWay<float> CreateWrappedObject(IReadOnlyStat<float> model, IView<float> view)
            => new BinderOneWay<float>(model, view);
    }
}
