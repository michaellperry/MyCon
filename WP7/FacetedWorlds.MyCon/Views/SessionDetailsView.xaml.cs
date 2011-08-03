using System.Windows;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;
using UpdateControls;
using System;
using Microsoft.Phone.Shell;

namespace FacetedWorlds.MyCon.Views
{
    public partial class SessionDetailsView : PhoneApplicationPage
    {
        private Dependent _depAddButtonEnabled;
        private Dependent _depRemoveButtonEnabled;
        private Dependent _depEvaluationButtonEnabled;
        private Dependent _depSearchBySpeakerText;
        private Dependent _depSearchBySpeakerEnabled;
        private Dependent _depSearchByTrackText;
        private Dependent _depSearchByTrackEnabled;

        public SessionDetailsView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string sessionId = NavigationContext.QueryString["SessionId"];
            ViewModelLocator locator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (locator != null)
                DataContext = locator.GetSessionDetailsViewModel(sessionId);

            _depAddButtonEnabled = this.UpdateWhenNecessary(() => this.Button(0).IsEnabled = CanAdd);
            _depRemoveButtonEnabled = this.UpdateWhenNecessary(() => this.Button(1).IsEnabled = CanRemove);
            _depEvaluationButtonEnabled = this.UpdateWhenNecessary(() => this.Button(2).IsEnabled = CanEvaluate);
            _depSearchBySpeakerText = this.UpdateWhenNecessary(() => this.MenuItem(0).Text = SearchBySpeakerText);
            _depSearchBySpeakerEnabled = this.UpdateWhenNecessary(() => this.MenuItem(0).IsEnabled = CanSearchBySpeaker);
            _depSearchByTrackText = this.UpdateWhenNecessary(() => this.MenuItem(1).Text = SearchByTrackText);
            _depSearchByTrackEnabled = this.UpdateWhenNecessary(() => this.MenuItem(1).IsEnabled = CanSearchByTrack);
        }


        private bool CanAdd
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanAdd;
                return false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                viewModel.Add();
            if (ShouldGoToSettings(viewModel))
                NavigationService.Navigate(new Uri("/Views/SettingsView.xaml", UriKind.Relative));
            else
                NavigationService.GoBack();
        }

        private bool CanRemove
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanRemove;
                return false;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                viewModel.Remove();
            NavigationService.GoBack();
        }

        public bool CanEvaluate
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanEvaluate;
                return false;
            }
        }

        private void Evaluation_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                NavigationService.Navigate(new Uri(viewModel.SessionEvaluationUri, UriKind.Relative));
        }

        private string SearchBySpeakerText
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.SearchBySpeakerText;
                else
                    return "Other sessions by speaker";
            }
        }

        public bool CanSearchBySpeaker
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanSearchBySpeaker;
                else
                    return false;
            }
        }

        private void SessionsBySpeaker_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
            {
                NavigationService.Navigate(new Uri(String.Format("/Views/SpeakerView.xaml?SpeakerId={0}", Uri.EscapeUriString(viewModel.SpeakerId)), UriKind.Relative));
            }
        }

        private string SearchByTrackText
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.SearchByTrackText;
                else
                    return "Other sessions in track";
            }
        }

        public bool CanSearchByTrack
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanSearchByTrack;
                return false;
            }
        }

        private void SessionsByTrack_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                viewModel.SearchByTrack();
            NavigationService.Navigate(new Uri("/Views/TracksView.xaml", UriKind.Relative));
        }

        private static bool ShouldGoToSettings(SessionDetailsViewModel viewModel)
        {
            if (viewModel != null && viewModel.ShouldPromptForPushNotification())
            {
                MessageBoxResult result = MessageBox.Show(
                    "Would you like to enable push notifications to stay informed of schedule changes?",
                    viewModel.GetConferenceName(),
                    MessageBoxButton.OKCancel);
                return result == MessageBoxResult.OK;
            }
            return false;
        }
    }
}