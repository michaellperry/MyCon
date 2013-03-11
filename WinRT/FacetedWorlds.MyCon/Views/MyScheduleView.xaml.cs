using FacetedWorlds.MyCon.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FacetedWorlds.MyCon.Views
{
    public sealed partial class MyScheduleView : UserControl, ILayoutAwareControl
    {
        public MyScheduleView()
        {
            this.InitializeComponent();
        }

        public void SetLayout(ApplicationViewState viewState)
        {
            SnappedView.Visibility = viewState == ApplicationViewState.Snapped
                ? Windows.UI.Xaml.Visibility.Visible
                : Windows.UI.Xaml.Visibility.Collapsed;
            FullView.Visibility = viewState == ApplicationViewState.Snapped
                ? Windows.UI.Xaml.Visibility.Collapsed
                : Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
