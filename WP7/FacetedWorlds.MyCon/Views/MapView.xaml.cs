using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Media;

namespace FacetedWorlds.MyCon.Views
{
    public partial class MapView : PhoneApplicationPage
    {
        public MapView()
        {
            InitializeComponent();
        }

        private void MapImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MatrixTransform transform = RenderTransform as MatrixTransform;
            if (transform != null)
            {
                double scale = 0.5;
                transform.Matrix = new Matrix(scale, 0.0, 0.0, scale, 0.0, 0.0);
            }
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MatrixTransform transform = RenderTransform as MatrixTransform;
            if (transform != null)
            {
                double scale = 0.5;
                transform.Matrix = new Matrix(scale, 0.0, 0.0, scale, 0.0, 0.0);
            }
        }
    }
}