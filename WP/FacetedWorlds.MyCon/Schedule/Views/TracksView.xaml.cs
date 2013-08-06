using System.Windows;
using System.Windows.Controls;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.Schedule.ViewModels;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public partial class TracksView : PhoneApplicationPage
    {
        public TracksView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<TracksViewModel>(DataContext);
            if (viewModel != null)
                TracksPivot.SelectedIndex = viewModel.SelectedTrackIndex;
        }
    }
}