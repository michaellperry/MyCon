using System;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.ViewModels;

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

        private void Search_Click(object sender, EventArgs e)
        {
            ScheduleViewModel viewModel = ForView.Unwrap<ScheduleViewModel>(DataContext);
            if (viewModel != null)
                viewModel.ClearSearch();
            NavigationService.Navigate(new Uri("/Views/SearchView.xaml", UriKind.Relative));
        }
    }
}