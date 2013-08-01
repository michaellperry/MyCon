using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.Conferences.Views
{
    public partial class ConferenceListView : UserControl
    {
        public ConferenceListView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<ConferenceListViewModel>(DataContext);
            if (viewModel == null)
                return;

            viewModel.ConferenceSelected += ViewModel_ConferenceSelected;
        }

        void ViewModel_ConferenceSelected()
        {
            App.RootFrame.Navigate(new Uri("/Conferences/Views/ConferenceDetailsPage.xaml", UriKind.Relative));
        }
    }
}
