using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.Views
{
    public class SessionUserControl : UserControl
    {
        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            if (e.TotalManipulation.Translation.X == 0 && e.TotalManipulation.Translation.Y == 0)
            {
                SessionViewModelBase viewModel = ForView.Unwrap<SessionViewModelBase>(DataContext);
                if (viewModel != null)
                {
                    string targetUri = viewModel.TargetUri;
                    if (targetUri != null)
                        // TODO: Still a nasty hack, but at least now it's in a nasty place.
                        ((PhoneApplicationFrame)(Application.Current.RootVisual)).Navigate(new Uri(targetUri, UriKind.Relative));
                }
            }

            base.OnManipulationCompleted(e);
        }
    }
}
