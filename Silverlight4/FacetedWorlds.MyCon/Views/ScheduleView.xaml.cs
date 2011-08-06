using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FacetedWorlds.MyCon.ViewModels;
using SL_Drag_Drop_BaseClasses;
using UpdateControls.XAML;
using System.Windows.Threading;

namespace FacetedWorlds.MyCon.Views
{
    public partial class ScheduleView : UserControl
    {
        private const double ScrollBy = 50.0;
        private const double ScrollEdge = 8.0;

        private DispatcherTimer _timer = new DispatcherTimer();
        private Point _position;

        public ScheduleView()
        {
            InitializeComponent();

            InitialValues.ContainingLayoutPanel = this.LayoutRoot;

            _timer.Interval = TimeSpan.FromSeconds(0.25);
            _timer.Tick += Timer_Tick;
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_position.X < ScrollEdge)
                Scroller.ScrollToHorizontalOffset(Scroller.HorizontalOffset - ScrollBy);
            else if (_position.X > Scroller.ViewportWidth - ScrollEdge)
                Scroller.ScrollToHorizontalOffset(Scroller.HorizontalOffset + ScrollBy);
            if (_position.Y < ScrollEdge)
                Scroller.ScrollToVerticalOffset(Scroller.VerticalOffset - ScrollBy);
            else if (_position.Y > Scroller.ViewportHeight - ScrollEdge)
                Scroller.ScrollToVerticalOffset(Scroller.VerticalOffset + ScrollBy);
        }

        private void DragSource_DragStarted(object sender, SL_Drag_Drop_BaseClasses.DragEventArgs args)
        {
            _timer.Start();
            _position = args.MouseEventArgs.GetPosition(Scroller);
        }

        private void DragSource_DragFinished(object sender, SL_Drag_Drop_BaseClasses.DragEventArgs args)
        {
            _timer.Stop();
        }

        private void DragSource_DragMoved(object sender, SL_Drag_Drop_BaseClasses.DragEventArgs args)
        {
            _position = args.MouseEventArgs.GetPosition(Scroller);
        }
    }
}
