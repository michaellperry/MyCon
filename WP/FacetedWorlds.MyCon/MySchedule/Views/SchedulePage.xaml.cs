using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FacetedWorlds.MyCon.MySchedule.ViewModels;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.MySchedule.Views
{
    public partial class SchedulePage : PhoneApplicationPage
    {
        public SchedulePage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator locator = App.Current.Resources["Locator"] as ViewModelLocator;
            if (locator == null)
                return;

            Guid conferenceId;
            if (!Guid.TryParse(NavigationContext.QueryString["ConferenceId"], out conferenceId))
                return;

            DataContext = locator.GetScheduleViewModel(conferenceId);
        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }

        private void Tracks_Click(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void Map_Click(object sender, EventArgs e)
        {

        }

        private void Leave_Click(object sender, EventArgs e)
        {
            var viewModel = ForView.Unwrap<ScheduleViewModel>(DataContext);
            if (viewModel == null)
                return;

            viewModel.LeaveConference();

            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}