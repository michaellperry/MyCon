using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FacetedWorlds.MyCon.Schedule.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public partial class ScheduleSessionUserControl : UserControl
    {
        public ScheduleSessionUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var viewModel = ForView.Unwrap<ScheduleSlotViewModel>(DataContext);
            if (viewModel == null)
                return;

            Guid conferenceId = viewModel.ConferenceId;
            Guid timeId = viewModel.TimeId;
            var targetUri = String.Format("/Schedule/Views/SlotView.xaml?ConferenceId={1}&TimeId={0:d}", timeId, conferenceId);
            ((PhoneApplicationFrame)(Application.Current.RootVisual)).Navigate(new Uri(targetUri, UriKind.Relative));
        }
    }
}
