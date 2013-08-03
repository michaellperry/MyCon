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
    public partial class ConferenceDetailsPage : PhoneApplicationPage
    {
        public ConferenceDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var viewModel = ForView.Unwrap<ConferenceDetailsViewModel>(DataContext);

            if (viewModel != null)
            {
                viewModel.NavigatedFrom();
            }

            base.OnNavigatedFrom(e);
        }
    }
}