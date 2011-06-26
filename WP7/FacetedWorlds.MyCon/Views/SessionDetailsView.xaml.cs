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

            _depAddButtonEnabled = UpdateWhenNecessary(() => ApplicationBarButton(0).IsEnabled = CanAdd);
            _depRemoveButtonEnabled = UpdateWhenNecessary(() => ApplicationBarButton(1).IsEnabled = CanRemove);
        }


        private bool CanAdd
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return !viewModel.IsScheduled;
                return false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                viewModel.Add();
        }

        private bool CanRemove
        {
            get
            {
                SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.IsScheduled;
                return false;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            SessionDetailsViewModel viewModel = ForView.Unwrap<SessionDetailsViewModel>(DataContext);
            if (viewModel != null)
                viewModel.Remove();
        }

        private void Evaluation_Click(object sender, EventArgs e)
        {

        }

        private void SessionsBySpeaker_Click(object sender, EventArgs e)
        {

        }

        private void SessionsByTrack_Click(object sender, EventArgs e)
        {

        }

        private Dependent UpdateWhenNecessary(Action update)
        {
            Dependent dependent = new Dependent(update);
            dependent.Invalidated +=
                () => Dispatcher.BeginInvoke(
                    () => dependent.OnGet());
            dependent.OnGet();
            return dependent;
        }

        private ApplicationBarIconButton ApplicationBarButton(int index)
        {
            return (ApplicationBarIconButton)ApplicationBar.Buttons[index];
        }
    }
}