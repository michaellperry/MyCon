using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FacetedWorlds.MyCon.ViewModels;
using UpdateControls.XAML;
using System.Windows.Threading;
using System.Windows.Media;
using System.Collections.Generic;

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

            _timer.Interval = TimeSpan.FromSeconds(0.25);
            _timer.Tick += Timer_Tick;
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

        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {
            List<FrameworkElement> rowHeaders = ItemsIn(RowHeaderContainer);
            List<FrameworkElement> rows = ItemsIn(RowContainer);
            for (int i = 0; i < rowHeaders.Count && i < rows.Count; i++)
            {
                rowHeaders[i].Height = rows[i].ActualHeight;
            }

            ColumnHeaders.ScrollToHorizontalOffset(Scroller.HorizontalOffset);
            RowHeaders.ScrollToVerticalOffset(Scroller.VerticalOffset);
        }

        private List<FrameworkElement> ItemsIn(DependencyObject container)
        {
            DependencyObject itemsPresenter = VisualTreeHelper.GetChild(container, 0);
            DependencyObject itemsPanel = VisualTreeHelper.GetChild(itemsPresenter, 0);
            int itemCount = VisualTreeHelper.GetChildrenCount(itemsPanel);
            List<FrameworkElement> items = new List<FrameworkElement>(itemCount);
            for (int i = 0; i < itemCount; i++)
            {
                items.Add(VisualTreeHelper.GetChild(itemsPanel, i) as FrameworkElement);
            }
            return items;
        }
    }
}
