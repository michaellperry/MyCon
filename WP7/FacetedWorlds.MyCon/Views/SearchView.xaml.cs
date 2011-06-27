using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Views
{
    public partial class SearchView : PhoneApplicationPage
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SearchTermTextBox.Text))
                SearchTermTextBox.Focus();
        }

        private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Focus();
        }
    }
}