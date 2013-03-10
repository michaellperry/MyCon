using FacetedWorlds.MyCon.ViewModels.MySchedule;
using UpdateControls;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FacetedWorlds.MyCon.Controls.MySchedule
{
    public sealed partial class SnappedScheduleTimeControl : UserControl
    {
        private Dependent _visualState;

        public SnappedScheduleTimeControl()
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
            var viewModel = ForView.Unwrap<ScheduleTimeViewModel>(DataContext);
            if (viewModel == null)
                return;

            if (viewModel.IsSelected)
                VisualStateManager.GoToState(this, "Selected", true);
            else
                VisualStateManager.GoToState(this, "Unselected", true);
        }
    }
}
