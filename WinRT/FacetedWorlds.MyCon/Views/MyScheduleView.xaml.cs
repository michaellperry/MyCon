using FacetedWorlds.MyCon.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using UpdateControls;
using Windows.UI.Xaml;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.ViewModels.MySchedule;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FacetedWorlds.MyCon.Views
{
    public sealed partial class MyScheduleView : UserControl, ILayoutAwareControl
    {
        private Dependent _visualState;

        public MyScheduleView()
        {
            this.InitializeComponent();

            Loaded += ScheduleTimeControl_Loaded;
        }

        void ScheduleTimeControl_Loaded(object sender, RoutedEventArgs e)
        {
            _visualState = Update.WhenNecessary(UpdateVisualState);
        }

        private void UpdateVisualState()
        {
            var viewModel = ForView.Unwrap<MyScheduleViewModel>(DataContext);
            if (viewModel == null)
                return;

            if (viewModel.HasSelection)
                VisualStateManager.GoToState(this, "Selection", true);
            else
                VisualStateManager.GoToState(this, "NoSelection", true);
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
