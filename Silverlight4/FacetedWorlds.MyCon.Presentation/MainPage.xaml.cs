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
using FacetedWorlds.MyCon.Presentation.Navigation;

namespace FacetedWorlds.MyCon.Presentation
{
    public partial class MainPage : UserControl
    {
        private NavigationGraph _navigationGraph;

        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        public NavigationGraph NavigationGraph
        {
            get { return _navigationGraph; }
            set { _navigationGraph = value; }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown)
            {
                _navigationGraph.Forward();
            }
            else if (e.Key == Key.PageUp)
            {
                _navigationGraph.Backward();
            }
        }
    }
}
