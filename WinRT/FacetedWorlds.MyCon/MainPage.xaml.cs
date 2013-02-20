using FacetedWorlds.MyCon.ViewModels;
using FacetedWorlds.MyCon.Views;
using UpdateControls;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FacetedWorlds.MyCon
{
    public sealed partial class MainPage : Page
    {
        private AllSessionsView _allSessionsView;
        private MyScheduleView _myScheduleView;

        private Dependent _depAllSessionsView;
        private Dependent _depMyScheduleView;

        public MainPage()
        {
            this.InitializeComponent();

            _depAllSessionsView = Update.WhenNecessary(UpdateAllSessionsView);
            _depMyScheduleView = Update.WhenNecessary(UpdateMyScheduleView);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _depAllSessionsView.OnGet();
            _depMyScheduleView.OnGet();
        }

        private void SessionSelected()
        {
            this.Frame.Navigate(typeof(SessionView));
        }

        private void MySchedule_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return;

            viewModel.SelectedView = MainViewModel.ViewOption.MyScheduleView;
        }

        private void AllSessions_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return;

            viewModel.SelectedView = MainViewModel.ViewOption.AllSessionsView;
        }

        private void UpdateAllSessionsView()
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return;

            bool visible = viewModel.SelectedView == MainViewModel.ViewOption.AllSessionsView;

            if (visible && _allSessionsView == null)
            {
                _allSessionsView = new AllSessionsView();
                _allSessionsView.SessionSelected += SessionSelected;
                Container.Children.Add(_allSessionsView);
            }

            if (!visible && _allSessionsView != null)
            {
                _allSessionsView.SessionSelected -= SessionSelected;
                Container.Children.Remove(_allSessionsView);
                _allSessionsView = null;
            }
        }

        private void UpdateMyScheduleView()
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                return;

            bool visible = viewModel.SelectedView == MainViewModel.ViewOption.MyScheduleView;

            if (visible && _myScheduleView == null)
            {
                _myScheduleView = new MyScheduleView();
                Container.Children.Add(_myScheduleView);
            }

            if (!visible && _myScheduleView != null)
            {
                Container.Children.Remove(_myScheduleView);
                _myScheduleView = null;
            }
        }
    }
}
