using System.Collections.Generic;
using FacetedWorlds.MyCon.Common;
using FacetedWorlds.MyCon.ViewModels;
using FacetedWorlds.MyCon.Views;
using UpdateControls;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace FacetedWorlds.MyCon
{
    public sealed partial class MainPage : LayoutAwarePage
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
            if (e.Parameter != null)
            {
                var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
                viewModel.PerformSearch(e.Parameter.ToString());
            }
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
                viewModel = DataContext as MainViewModel;
            if (viewModel == null)
                return;

            viewModel.SelectedView = MainViewModel.ViewOption.MyScheduleView;
        }

        private void AllSessions_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                viewModel = DataContext as MainViewModel;
            if (viewModel == null)
                return;

            viewModel.SelectedView = MainViewModel.ViewOption.AllSessionsView;
            viewModel.ClearSearch();
        }

        private void UpdateAllSessionsView()
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                viewModel = DataContext as MainViewModel;
            if (viewModel == null)
                return;

            bool visible = viewModel.SelectedView == MainViewModel.ViewOption.AllSessionsView;

            if (visible && _allSessionsView == null)
            {
                _allSessionsView = new AllSessionsView();
                Container.Children.Add(_allSessionsView);
                StartLayoutUpdates(_allSessionsView);
                _allSessionsView.SessionSelected += SessionSelected;
            }

            if (!visible && _allSessionsView != null)
            {
                _allSessionsView.SessionSelected -= SessionSelected;
                StopLayoutUpdates(_allSessionsView);
                Container.Children.Remove(_allSessionsView);
                _allSessionsView = null;
            }
        }

        private void UpdateMyScheduleView()
        {
            var viewModel = ForView.Unwrap<MainViewModel>(DataContext);
            if (viewModel == null)
                viewModel = DataContext as MainViewModel;
            if (viewModel == null)
                return;

            bool visible = viewModel.SelectedView == MainViewModel.ViewOption.MyScheduleView;

            if (visible && _myScheduleView == null)
            {
                _myScheduleView = new MyScheduleView();
                Container.Children.Add(_myScheduleView);
                StartLayoutUpdates(_myScheduleView);
            }

            if (!visible && _myScheduleView != null)
            {
                StopLayoutUpdates(_myScheduleView);
                Container.Children.Remove(_myScheduleView);
                _myScheduleView = null;
            }
        }
    }
}
