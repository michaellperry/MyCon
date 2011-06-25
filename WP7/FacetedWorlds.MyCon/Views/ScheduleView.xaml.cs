using System;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Views
{
    public partial class ScheduleView : PhoneApplicationPage
    {
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void Tracks_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/TracksView.xaml", UriKind.Relative));
        }
    }
}