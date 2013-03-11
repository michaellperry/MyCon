using Windows.UI.ViewManagement;

namespace FacetedWorlds.MyCon.Common
{
    public interface ILayoutAwareControl
    {
        void SetLayout(ApplicationViewState viewState);
    }
}
