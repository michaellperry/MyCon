using System.Windows;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public partial class SpeakerView : PhoneApplicationPage
    {
        public SpeakerView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string speakerId = NavigationContext.QueryString["SpeakerId"] as string;
            ViewModelLocator locator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (locator != null)
                DataContext = locator.GetSpeakerViewModel(speakerId);
        }
    }
}