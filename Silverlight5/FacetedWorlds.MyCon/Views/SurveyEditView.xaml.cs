using System;
using System.Windows.Controls;

namespace FacetedWorlds.MyCon.Views
{
    public partial class SurveyEditView : UserControl
    {
        public event EventHandler SaveClicked;

        public SurveyEditView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SaveClicked != null)
                SaveClicked(sender, e);
        }
    }
}
