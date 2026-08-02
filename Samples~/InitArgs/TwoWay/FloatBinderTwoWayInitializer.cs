using Sisus.Init;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for FloatBinderTwoWay.
    /// </summary>
    public class FloatBinderTwoWayWrapper : Wrapper<BinderTwoWay<float>> { }

    /// <summary>
    /// InitArgs wrapper initializer for FloatBinderTwoWay.
    /// </summary>
    public class FloatBinderTwoWayInitializer : WrapperInitializer<FloatBinderTwoWayWrapper, BinderTwoWay<float>, IStat<float>, ITwoWayView<float>>
    {
        protected override BinderTwoWay<float> CreateWrappedObject(IStat<float> model, ITwoWayView<float> view)
            => new BinderTwoWay<float>();
    }
}
