using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.ViewModels;

namespace FacetedWorlds.MyCon.Views
{
    public partial class ConferenceView : UserControl
    {
        public ConferenceView()
        {
            InitializeComponent();
        }

        private void EditSurvey_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ConferenceViewModel dataContext = ForView.Unwrap<ConferenceViewModel>(this.DataContext);
            if (dataContext != null)
            {
                SurveyEditChildWindow childWindow = new SurveyEditChildWindow();
                childWindow.DataContext = ForView.Wrap(dataContext.SurveyEdit);
                childWindow.Show();
            }
        }
    }
}
