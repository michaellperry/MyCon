using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FacetedWorlds.MyCon.ViewModels;
using Microsoft.Phone.Controls;
using UpdateControls.XAML;
using System.Windows.Media;
using FacetedWorlds.MyCon.Schedule.ViewModels;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public class SessionUserControl : UserControl
    {
        private const double RadiansToDegrees = 180.0 / Math.PI;

        private bool _mouseDown = false;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            ShowTilt(e.GetPosition(this));
            _mouseDown = true;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mouseDown)
                ShowTilt(e.GetPosition(this));
            base.OnMouseMove(e);
        }

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            SetProjection(0.0, 0.0);
            _mouseDown = false;
            if (e.TotalManipulation.Translation.X == 0 && e.TotalManipulation.Translation.Y == 0)
            {
                SessionViewModelBase viewModel = ForView.Unwrap<SessionViewModelBase>(DataContext);
                if (viewModel != null)
                {
                    string targetUri = viewModel.TargetUri;
                    if (targetUri != null)
                        // TODO: Still a nasty hack, but at least now it's in a nasty place.
                        ((PhoneApplicationFrame)(Application.Current.RootVisual)).Navigate(new Uri(targetUri, UriKind.Relative));
                }
            }

            base.OnManipulationCompleted(e);
        }

        private void ShowTilt(Point point)
        {
            double halfWidth = ActualWidth / 2;
            double halfHeight = ActualHeight / 2;

            double xAngle = 0.0;
            if (point.Y > 0.0 && point.Y < ActualHeight)
                xAngle = Math.Asin((point.Y - halfHeight) / halfHeight) * RadiansToDegrees / 4.0;

            double yAngle = 0.0;
            if (point.X > 0.0 && point.X < ActualWidth)
                yAngle = Math.Asin(-(point.X - halfWidth) / halfWidth) * RadiansToDegrees / 4.0;

            SetProjection(xAngle, yAngle);
        }

        private void SetProjection(double xAngle, double yAngle)
        {
            PlaneProjection projection = Projection as PlaneProjection;
            if (projection == null)
            {
                projection = new PlaneProjection();
                Projection = projection;
            }
            projection.RotationX = xAngle;
            projection.RotationY = yAngle;
        }
    }
}
