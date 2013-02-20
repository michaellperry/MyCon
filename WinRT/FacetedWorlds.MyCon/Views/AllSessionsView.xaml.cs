using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FacetedWorlds.MyCon.ViewModels.AllSessions;
using UpdateControls.XAML;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FacetedWorlds.MyCon.Views
{
    public sealed partial class AllSessionsView : UserControl
    {
        public event Action SessionSelected;

        public AllSessionsView()
        {
            this.InitializeComponent();
        }

        private void Session_ItemClick(object sender, ItemClickEventArgs e)
        {
            var viewModel = ForView.Unwrap<SessionHeaderViewModel>(e.ClickedItem);
            viewModel.Select();
            if (SessionSelected != null)
                SessionSelected();
        }
    }
}
