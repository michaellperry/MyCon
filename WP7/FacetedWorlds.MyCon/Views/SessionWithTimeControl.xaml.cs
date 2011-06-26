using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon
{
	public partial class SessionWithTimeControl : UserControl
	{
		public SessionWithTimeControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        private void UserControl_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
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
        }
	}
}