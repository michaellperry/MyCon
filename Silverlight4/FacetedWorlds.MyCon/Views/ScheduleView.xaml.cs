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
using SL_Drag_Drop_BaseClasses;
using System.Diagnostics;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.ViewModels;

namespace FacetedWorlds.MyCon.Views
{
    public partial class ScheduleView : UserControl
    {
        public ScheduleView()
        {
            InitializeComponent();

            InitialValues.ContainingLayoutPanel = this.LayoutRoot;
        }

        private void DropTarget_DragSourceDropped(object sender, DropEventArgs args)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            if (senderElement != null)
            {
                ScheduledSessionViewModel source = ForView.Unwrap<ScheduledSessionViewModel>(args.DragSource.DataContext);
                ScheduleCellViewModel target = ForView.Unwrap<ScheduleCellViewModel>(senderElement.DataContext);
                if (source != null && target != null)
                {
                    source.MoveTo(target.Place);
                    Debug.WriteLine(String.Format("{0} dropped on {1}.", source, target));
                }
            }
        }
    }
}
