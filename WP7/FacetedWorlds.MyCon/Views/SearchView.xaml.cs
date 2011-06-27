using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Input;

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
            SearchTermTextBox.Focus();
        }

        private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Focus();
        }
    }
}