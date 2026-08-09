using Sisus.Init;
using DesignPatterns.UI.MVP;

namespace DesignPatterns.UI.MVP.InitArgs
{
    /// <summary>
    /// InitArgs wrapper for IntBinderOneWay.
    /// </summary>
    public class IntBinderOneWayWrapper : Wrapper<BinderOneWay<int>> { }

    /// <summary>
    /// InitArgs wrapper initializer for IntBinderOneWay.
    /// </summary>
    public class IntBinderOneWayInitializer : WrapperInitializer<IntBinderOneWayWrapper, BinderOneWay<int>, IReadOnlyStat<int>, IView<int>>
    {
        protected override BinderOneWay<int> CreateWrappedObject(IReadOnlyStat<int> model, IView<int> view)
            => new BinderOneWay<int>(model, view);
    }
}
