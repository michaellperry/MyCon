using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.Conferences.Views
{
    public partial class ConferenceDetailsPage : PhoneApplicationPage
    {
        public ConferenceDetailsPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var locator = App.Current.Resources["Locator"] as ViewModelLocator;
            if (locator == null)
                return;

            Guid conferenceId;
            if (!Guid.TryParse(NavigationContext.QueryString["ConferenceId"], out conferenceId))
                return;

            DataContext = locator.GetConferenceDetailsViewModel(conferenceId);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var viewModel = ForView.Unwrap<ConferenceDetailsViewModel>(DataContext);

            if (viewModel != null)
            {
                viewModel.NavigatedFrom();
            }

            base.OnNavigatedFrom(e);
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            var viewModel = ForView.Unwrap<ConferenceDetailsViewModel>(DataContext);
            if (viewModel == null)
                return;

            Guid id = viewModel.JoinConference();

            NavigationService.Navigate(new Uri(
                String.Format(
                    "/MySchedule/Views/SchedulePage.xaml?ConferenceId={0}",
                    id),
                UriKind.Relative));
        }
    }
}