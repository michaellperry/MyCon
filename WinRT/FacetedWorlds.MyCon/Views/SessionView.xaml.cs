using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FacetedWorlds.MyCon.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FacetedWorlds.MyCon.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionView : LayoutAwarePage
    {
        public SessionView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public override void SetLayout(ApplicationViewState viewState)
        {
            SnappedView.Visibility = viewState == ApplicationViewState.Snapped
                ? Visibility.Visible
                : Visibility.Collapsed;
            FullView.Visibility = viewState == ApplicationViewState.Snapped
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}
