using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace FacetedWorlds.MyCon.Controls
{
    public partial class RatingControl : UserControl
    {
        private const int StarSize = 78;

        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(int),
            typeof(RatingControl),
            new PropertyMetadata((control, args) =>
                ((RatingControl)control).ValueChanged((int)args.NewValue)));

        public RatingControl()
        {
            InitializeComponent();
        }

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            if (e.TotalManipulation.Translation.X == 0.0 && e.TotalManipulation.Translation.Y == 0.0)
                HandleMouseEvent(e.ManipulationOrigin);

            base.OnManipulationCompleted(e);
        }

        private void HandleMouseEvent(Point position)
        {
            int index = (int)(position.X) / StarSize + 1;
            if (index < 1)
                index = 1;
            if (index > 5)
                index = 5;
            Value = index;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public void ValueChanged(int value)
        {
            Star1.Filled = value >= 1;
            Star2.Filled = value >= 2;
            Star3.Filled = value >= 3;
            Star4.Filled = value >= 4;
            Star5.Filled = value >= 5;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ValueChanged(Value);
        }
    }
}
