using System.Windows;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UpdateControls;
using UpdateControls.Fields;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon
{
    public partial class MainPage : PhoneApplicationPage, IUpdatable
    {
        private Dependent<string> _visibleAppBar;

        public MainPage()
        {
            InitializeComponent();

            _visibleAppBar = new Dependent<string>(() => VisibleAppBar());
            _visibleAppBar.Invalidated += () => UpdateScheduler.ScheduleUpdate(this);
            UpdateNow();
        }

        public void UpdateNow()
        {
            this.ApplicationBar = Resources[_visibleAppBar.Value] as ApplicationBar;
        }

        private string VisibleAppBar()
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return null;

            if (viewModel.ConferencesVisibility == Visibility.Visible)
                return "ConferencesApplicationBar";
            if (viewModel.MyScheduleVisibility == Visibility.Visible)
                return "MyScheduleApplicationBar";
            return null;
        }

        private void Leave_Click(object sender, System.EventArgs e)
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return;

            viewModel.LeaveConference();
        }
    }
}