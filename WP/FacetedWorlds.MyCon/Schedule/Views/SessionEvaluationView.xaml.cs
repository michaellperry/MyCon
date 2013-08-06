using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;
using UpdateControls;
using System;
using FacetedWorlds.MyCon.Schedule.ViewModels;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public partial class SessionEvaluationView : PhoneApplicationPage
    {
        private Dependent _depSubmitEnabled;

        public SessionEvaluationView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string sessionId = NavigationContext.QueryString["SessionId"];
            ViewModelLocator locator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (locator != null)
                DataContext = locator.GetSessionEvaluationViewModel(sessionId);

            _depSubmitEnabled = this.UpdateWhenNecessary(() => this.Button(0).IsEnabled = CanSubmit);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CommitText();
            base.OnNavigatingFrom(e);
        }

        public bool CanSubmit
        {
            get
            {
                SessionEvaluationViewModel viewModel = ForView.Unwrap<SessionEvaluationViewModel>(DataContext);
                if (viewModel != null)
                    return viewModel.CanSubmit;
                return false;
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            CommitText();
            SessionEvaluationViewModel viewModel = ForView.Unwrap<SessionEvaluationViewModel>(DataContext);
            viewModel.Submit();
            NavigationService.GoBack();
        }

        private static void CommitText()
        {
            TextBox focusTextBox = FocusManager.GetFocusedElement() as TextBox;
            if (focusTextBox != null)
                focusTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}