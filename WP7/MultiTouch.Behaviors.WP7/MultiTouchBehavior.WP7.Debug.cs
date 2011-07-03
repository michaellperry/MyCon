// ****************************************************************************
// <copyright file="MultiTouchBehavior.WP7.Debug.cs" company="Laurent Bugnion">
// Copyright © Laurent Bugnion 2010
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>20.6.2010</date>
// <project>MultiTouch.Behaviors.WP7</project>
// <web>http://multitouch.codeplex.com/</web>
// <license>
// See http://multitouch.codeplex.com/license.
// </license>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MultiTouch.Behaviors.WP7
{
#if DEBUG
    public partial class MultiTouchBehavior
    {
        private enum DebugInfo
        {
            [Description("Scale: {0:N2}")]
            Scale,
            [Description("Rotate: {0:N2}")]
            Rotate,
            [Description("Translate X | Y: {0:N2} | {1:N2}")]
            Translate,
            [Description("Finger: {0}")]
            FingerPosition,
            [Description("Active: {0}")]
            IsManipulationActive,
            [Description("S | R | Tx | Ty on: {0} | {1} | {2} | {3}")]
            ScaleRotateTranslateEnabled,
            [Description("MinScale: {0}")]
            MinimumScale,
            [Description("MaxScale: {0}")]
            MaximumScale,
        }

        private const double DebugMarkerSize = 100;
        private const string DebugTextPrefix = "DebugText";
        private bool _attached;
        private Canvas _debugCanvas;
        private readonly List<Ellipse> _fingerMarkers = new List<Ellipse>();

        private void OutputDebugInfo(DebugInfo info, params object[] values)
        {
            if (!_isDebugModeActive
                || _debugCanvas == null)
            {
                return;
            }

            var text = _debugCanvas.FindName(DebugTextPrefix + info) as TextBlock;
            if (text == null
                || text.Tag == null)
            {
                return;
            }

            text.Text = string.Format(text.Tag.ToString(), values);
        }

        private void OutputInitialDebugInfo(DebugInfo index, TextBlock value)
        {
            if (!_isDebugModeActive)
            {
                return;
            }

            ManipulationStarted += (s, e) => OutputDebugInfo(DebugInfo.IsManipulationActive, true);
            ManipulationCompleted += (s, e) => OutputDebugInfo(DebugInfo.IsManipulationActive, false);

            switch (index)
            {
                case DebugInfo.Scale:
                    value.Text = string.Format(value.Tag.ToString(), 1);
                    break;
                case DebugInfo.Rotate:
                    value.Text = string.Format(value.Tag.ToString(), 0);
                    break;
                case DebugInfo.Translate:
                    value.Text = string.Format(value.Tag.ToString(), 0, 0);
                    break;
                case DebugInfo.ScaleRotateTranslateEnabled:
                    value.Text = string.Format(
                        value.Tag.ToString(), 
                        IsScaleEnabled, 
                        IsRotateEnabled, 
                        IsTranslateXEnabled,
                        IsTranslateYEnabled);
                    break;
                case DebugInfo.MaximumScale:
                    value.Text = string.Format(value.Tag.ToString(), MaximumScale);
                    break;
                case DebugInfo.MinimumScale:
                    value.Text = string.Format(value.Tag.ToString(), MinimumScale);
                    break;
                case DebugInfo.IsManipulationActive:
                    value.Text = string.Format(value.Tag.ToString(), false);
                    break;
            }
        }

        public void AttachDebugMode()
        {
            if (_debugCanvas == null)
            {
                var parent = VisualTreeHelper.GetParent(AssociatedObject);
                Panel rootPanel = null;
                while (parent != null)
                {
                    var parentParent = VisualTreeHelper.GetParent(parent);

                    if (parentParent != null
                        && parentParent is PhoneApplicationPage)
                    {
                        rootPanel = parent as Panel;
                        break;
                    }

                    parent = parentParent;
                }

                if (rootPanel != null
                    && (rootPanel is Grid
                        || rootPanel is Canvas))
                {
                    _debugCanvas = new Canvas
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        IsHitTestVisible = false
                    };

                    rootPanel.Children.Add(_debugCanvas);
                }
            }

            if (_debugCanvas == null)
            {
                throw new InvalidOperationException("Cannot enter debug mode, check the documentation");
            }

            if (_isDebugModeActive)
            {
                var foregroundBrush = new SolidColorBrush(Colors.Red);
                var backgroundBrush = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                var enumType = typeof(DebugInfo);

                for (var index = DebugInfo.Scale; index <= DebugInfo.MaximumScale; index++)
                {
                    var panel = new StackPanel
                    {
                        Height = 47,
                        Background = backgroundBrush,
                        Orientation = Orientation.Horizontal
                    };

                    var info = enumType.GetField(index.ToString());
                    var attribute = (DescriptionAttribute)info.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];

                    var value = new TextBlock
                    {
                        FontSize = 24,
                        Foreground = foregroundBrush,
                        Name = DebugTextPrefix + index,
                        Tag = attribute.Description
                    };

                    OutputInitialDebugInfo(index, value);

                    Canvas.SetTop(panel, (int)index * 47.0 + 50);

                    panel.Children.Add(value);
                    _debugCanvas.Children.Add(panel);
                }

                _attached = true;
            }
        }

        internal void HandleMockDebugInfoAndFingers(
            IEnumerable<ITouchPoint> touchpoints, 
            ITouchPoint primaryTouchPoint)
        {
            if (primaryTouchPoint == null
                || touchpoints == null
                || touchpoints.Count() == 0)
            {
                return;
            }

            CheckAttachDebugMode();
            ExecuteHandleDebugInfoAndFingers(
                touchpoints.Select(tp => tp.Position).ToArray(),
                touchpoints.Select(tp => tp.Action).ToArray(),
                primaryTouchPoint.Position);
        }
        
        private void HandleDebugInfoAndFingers(
            IEnumerable<TouchPoint> touchpoints, 
            TouchPoint primaryTouchPoint)
        {
            if (primaryTouchPoint == null
                || touchpoints == null
                || touchpoints.Count() == 0)
            {
                return;
            }

            CheckAttachDebugMode();
            ExecuteHandleDebugInfoAndFingers(
                touchpoints.Select(tp => tp.Position).ToArray(),
                touchpoints.Select(tp => tp.Action).ToArray(),
                primaryTouchPoint.Position);
        }

        private void CheckAttachDebugMode()
        {
            if (!_attached)
            {
                if (_isDebugModeActive || _areFingersVisible)
                {
                    AttachDebugMode();
                }
            }
        }

        private void ExecuteHandleDebugInfoAndFingers(
            IList<Point> positions, 
            IList<TouchAction> actions, 
            Point primaryPosition)
        {
            if (!_areFingersVisible)
            {
                return;
            }

            var lastIndex = 0;
            var debugInfo = string.Empty;

            for (var index = 0; index < positions.Count; index++)
            {
                var isMain = positions[index] == primaryPosition;

                if (_fingerMarkers.Count <= index)
                {
                    var fill = isMain
                        ? new SolidColorBrush(Colors.Red)
                        : new SolidColorBrush(Colors.Blue);

                    var debugMarker = new Ellipse
                    {
                        Height = DebugMarkerSize,
                        Width = DebugMarkerSize,
                        Opacity = 0.5,
                        IsHitTestVisible = false,
                        Fill = fill
                    };

                    _debugCanvas.Children.Add(debugMarker);
                    _fingerMarkers.Add(debugMarker);
                }

                if (actions[index] == TouchAction.Down
                    || actions[index] == TouchAction.Move)
                {
                    _fingerMarkers[index].Visibility = Visibility.Visible;

                    var centerPoint = positions[index];
                    
                    var actualPoint = new Point(
                        centerPoint.X - DebugMarkerSize / 2,
                        centerPoint.Y - DebugMarkerSize / 2);

                    Canvas.SetLeft(_fingerMarkers[index], actualPoint.X);
                    Canvas.SetTop(_fingerMarkers[index], actualPoint.Y);

                    if (isMain)
                    {
                        debugInfo += "[" + centerPoint + "]";
                    }
                    else
                    {
                        debugInfo += "(" + centerPoint + ")";
                    }
                }
                else
                {
                    _fingerMarkers[index].Visibility = Visibility.Collapsed;
                }

                lastIndex = index;
            }

            for (var index = lastIndex + 1; index < _fingerMarkers.Count; index++)
            {
                _fingerMarkers[index].Visibility = Visibility.Collapsed;
            }

            OutputDebugInfo(DebugInfo.FingerPosition, debugInfo);
        }
    }
#endif
}
