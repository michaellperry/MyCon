using FacetedWorlds.MyCon.ViewModels.MySchedule;
using UpdateControls;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FacetedWorlds.MyCon.Controls.MySchedule
{
    public sealed partial class SnappedScheduleSlotControl : UserControl
    {
        private Dependent _visualState;

        public SnappedScheduleSlotControl()
        {
            this.InitializeComponent();

            Loaded += ScheduleSlotControl_Loaded;
        }

        void ScheduleSlotControl_Loaded(object sender, RoutedEventArgs e)
        {
            _visualState = Update.WhenNecessary(UpdateVisualState);
        }

        private void UpdateVisualState()
        {
            var viewModel = ForView.Unwrap<ScheduleSlotViewModel>(DataContext);
            if (viewModel == null)
                return;

            if (viewModel.IsSelected)
                VisualStateManager.GoToState(this, "Selected", true);
            else
                VisualStateManager.GoToState(this, "Unselected", true);
        }
    }
}
