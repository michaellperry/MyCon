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

        private int _oldValue = 0;
        private double _startY = 0.0;
        private bool _editing = false;

        public RatingControl()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _editing = true;
            _oldValue = Value;
            Point position = e.GetPosition(this);
            _startY = position.Y;
            HandleMouseEvent(position);
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_editing)
            {
                Point position = e.GetPosition(this);
                if (Math.Abs(position.Y - _startY) > 10)
                    Reset();
                else
                    HandleMouseEvent(position);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _editing = false;
            base.OnMouseLeftButtonUp(e);
        }

        private void Reset()
        {
            _editing = false;
            Value = _oldValue;
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
