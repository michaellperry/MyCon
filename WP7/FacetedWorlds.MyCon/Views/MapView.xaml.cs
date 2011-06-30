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
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Views
{
    public partial class MapView : PhoneApplicationPage
    {
        private double _startWidth;
        private double _startHeight;
        private double _startTop;
        private double _startLeft;
        public MapView()
        {
            InitializeComponent();
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            //_startWidth = MapImage.ActualWidth;
            //_startHeight = MapImage.ActualHeight;
            //_startTop = Canvas.GetTop(MapImage);
            //_startLeft = Canvas.GetLeft(MapImage);
        }

        private void Image_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //Canvas.SetTop(MapImage, _startTop + e.CumulativeManipulation.Translation.Y);
            //Canvas.SetLeft(MapImage, _startLeft + e.CumulativeManipulation.Translation.X);

            //double scaleX = Math.Abs(e.CumulativeManipulation.Scale.X);
            //double scaleY = Math.Abs(e.CumulativeManipulation.Scale.Y);
            //if (scaleX > 0.0 && scaleY > 0.0)
            //{
            //    double scale = NearerToOne(scaleX, scaleY);
            //    MapImage.Width = _startWidth * scale;
            //    MapImage.Height = _startHeight * scale;
            //}
            //var image = MapImage;
            //var transform = image.RenderTransform as CompositeTransform;
            //if (transform == null)
            //{
            //    transform = new CompositeTransform();
            //    image.RenderTransform = transform;
            //}
            //transform.TranslateX += e.DeltaManipulation.Translation.X;
            //transform.TranslateY += e.DeltaManipulation.Translation.Y;
            //var scale = e.DeltaManipulation.Scale.X * e.DeltaManipulation.Scale.Y;
            //if (scale > 0.0)
            //{
            //    transform.ScaleX *= scale;
            //    transform.ScaleY *= scale;
            //}
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            //var image = MapImage;
            //var transform = image.RenderTransform as CompositeTransform;
            //if (transform == null)
            //{
            //    transform = new CompositeTransform();
            //    image.RenderTransform = transform;
            //}
            //Rect bounds = transform.TransformBounds(new Rect(0.0, 0.0, image.ActualWidth, image.ActualHeight));

            //if (bounds.Top > 0)
            //    transform.TranslateY -= bounds.Top;
            //if (bounds.Bottom < ContentPanel.ActualHeight)
            //    transform.TranslateY -= bounds.Bottom - ContentPanel.ActualHeight;
            //if (bounds.Left > 0)
            //    transform.TranslateX -= bounds.Left;
            //if (bounds.Right < ContentPanel.ActualWidth)
            //    transform.TranslateX -= bounds.Right - ContentPanel.ActualWidth;
        }

        //private static double NearerToOne(double scaleX, double scaleY)
        //{
        //    const double Epsilon = 0.0001;

        //    if (scaleX < Epsilon)
        //        return 1.0;
        //    if (scaleY < Epsilon)
        //        return 1.0;

        //    if (Math.Abs(Math.Log(scaleX)) < Math.Abs(Math.Log(scaleY)))
        //        return scaleX;
        //    else
        //        return scaleY;
        //}
    }
}