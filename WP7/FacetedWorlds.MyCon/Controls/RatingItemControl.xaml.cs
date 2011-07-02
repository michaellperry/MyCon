using System.Windows;
using System.Windows.Controls;

namespace FacetedWorlds.MyCon.Controls
{
    public partial class RatingItemControl : UserControl
    {
        public RatingItemControl()
        {
            InitializeComponent();
        }

        public bool Filled
        {
            get { return Fill.Visibility == Visibility.Visible; }
            set { Fill.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
