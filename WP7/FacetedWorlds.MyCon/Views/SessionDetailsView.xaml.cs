using System.Windows;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Views
{
    public partial class SessionDetailsView : PhoneApplicationPage
    {
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
        }
    }
}