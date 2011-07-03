// ****************************************************************************
// <copyright file="MultiTouchBehavior.WP7.cs" company="Laurent Bugnion">
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace MultiTouch.Behaviors.WP7
{
    /// <summary>
    /// Implements Multi-Touch Manipulation
    /// </summary>
    public partial class MultiTouchBehavior
    {
        private CompositeTransform _transform;
        private MultiTouchProcessor _processor;

        private void OnAttachedImpl()
        {
            _transform = new CompositeTransform();
            AssociatedObject.RenderTransform = _transform;

            _processor = new MultiTouchProcessor();
            _processor.Delta += OnProcessorDelta;
            _processor.IsScaleEnabled = IsScaleEnabled;
            _processor.IsRotateEnabled = IsRotateEnabled;
            _processor.IsTranslateXEnabled = IsTranslateXEnabled;
            _processor.IsTranslateYEnabled = IsTranslateYEnabled;
            _processor.MinimumScale = MinimumScale;
            _processor.MaximumScale = MaximumScale;

            AssociatedObject.SizeChanged += ImageSizeChanged;

#if DEBUG
            if (_isMockActive)
            {
                if (_mockTouch != null)
                {
                    _mockTouch.Detach(AssociatedObject);
                }

                _mockTouch = new MockTouch();
                _mockTouch.Attach(
                    AssociatedObject, 
                    this, 
                    _processor);
            }
            else
            {
                Touch.FrameReported += TouchFrameReported;
            }
#else
            Touch.FrameReported += TouchFrameReported;
#endif
        }

        void ImageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement parent = AssociatedObject.Parent as FrameworkElement;
            UIElement container = parent.Parent as UIElement;
            Size containerSize = container.RenderSize;
            _processor.ZoomOut(e.NewSize, containerSize);
        }

        private bool _isManipulationActive;

        public bool IsManipulationActive
        {
            get { return _isManipulationActive; }
        }

        private bool _eventWasRaised;

        private void OnProcessorDelta(object sender, EventArgs e)
        {
            _transform.ScaleX = _processor.Scale;
            _transform.ScaleY = _processor.Scale;
            _transform.Rotation = _processor.RotationDegrees;
            _transform.TranslateX = _processor.Offset.X;
            _transform.TranslateY = _processor.Offset.Y;

            if (!_eventWasRaised)
            {
                _isManipulationActive = true;
                if (RaiseManipulationStarted())
                {
                    _eventWasRaised = true;
                }
            }

#if DEBUG
            OutputDebugInfo(DebugInfo.Scale, _processor.Scale);
            OutputDebugInfo(DebugInfo.Rotate, _processor.RotationDegrees);
            OutputDebugInfo(DebugInfo.Translate, _processor.Offset.X, _processor.Offset.Y);
#endif
        }

        private PhoneApplicationFrame _rootVisual;

        internal UIElement RootVisual
        {
            get
            {
                TryAttachToRoot();
                return _rootVisual;
            }
        }

        private void OnDetachingImpl()
        {
            _rootVisual.MouseLeftButtonUp -= RootVisualMouseLeftButtonUp;

#if DEBUG

            if (_mockTouch != null)
            {
                _mockTouch.Detach(AssociatedObject);
                _mockTouch = null;
            }
#endif

            Touch.FrameReported -= TouchFrameReported;
        }

        private void RootVisualMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _eventWasRaised = false;
            _isManipulationActive = false;
            RaiseManipulationCompleted();
        }

        private static void OnIsScaleEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                behavior._processor.IsScaleEnabled = behavior.IsScaleEnabled;
#if DEBUG
                (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.ScaleRotateTranslateEnabled,
                behavior.IsScaleEnabled,
                behavior.IsRotateEnabled,
                behavior.IsTranslateXEnabled,
                behavior.IsTranslateYEnabled);
#endif
            }
        }

        private static void OnIsRotateEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                behavior._processor.IsRotateEnabled = behavior.IsRotateEnabled;
#if DEBUG
                (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.ScaleRotateTranslateEnabled,
                behavior.IsScaleEnabled,
                behavior.IsRotateEnabled,
                behavior.IsTranslateXEnabled,
                behavior.IsTranslateYEnabled);
#endif
            }
        }

        private static void OnIsTranslateXEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                behavior._processor.IsTranslateXEnabled = behavior.IsTranslateXEnabled;
#if DEBUG
                (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.ScaleRotateTranslateEnabled,
                behavior.IsScaleEnabled,
                behavior.IsRotateEnabled,
                behavior.IsTranslateXEnabled,
                behavior.IsTranslateYEnabled);
#endif
            }
        }

        private static void OnIsTranslateYEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                behavior._processor.IsTranslateYEnabled = behavior.IsTranslateYEnabled;
#if DEBUG
                (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.ScaleRotateTranslateEnabled,
                behavior.IsScaleEnabled,
                behavior.IsRotateEnabled,
                behavior.IsTranslateXEnabled,
                behavior.IsTranslateYEnabled);
#endif
            }
        }

        private static void OnMinimumScaleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                var newValue = (double)e.NewValue;
                if (behavior._transform.ScaleX < newValue)
                {
                    behavior._transform.ScaleX = newValue;
                }
                if (behavior._transform.ScaleY < newValue)
                {
                    behavior._transform.ScaleY = newValue;
                }
                if (behavior._processor != null)
                {
                    behavior._processor.MinimumScale = newValue;
                }
            }

#if DEBUG
            (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.MinimumScale,
                e.NewValue);
#endif
        }

        private static void OnMaximumScaleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MultiTouchBehavior;
            if (behavior != null
                && behavior._transform != null)
            {
                var newValue = (double)e.NewValue;
                if (behavior._transform.ScaleX > newValue)
                {
                    behavior._transform.ScaleX = newValue;
                }
                if (behavior._transform.ScaleY > newValue)
                {
                    behavior._transform.ScaleY = newValue;
                }
                if (behavior._processor != null)
                {
                    behavior._processor.MaximumScale = newValue;
                }
            }

#if DEBUG
            (sender as MultiTouchBehavior).OutputDebugInfo(
                DebugInfo.MaximumScale,
                e.NewValue);
#endif
        }

        private bool _isDebugModeActive;
        public bool IsDebugModeActive
        {
            get
            {
                return _isDebugModeActive;
            }
            set
            {
#if DEBUG
                _isDebugModeActive = value;
#endif
            }
        }

        private bool _areFingersVisible;
        public bool AreFingersVisible
        {
            get
            {
                return _areFingersVisible;
            }
            set
            {
#if DEBUG
                _areFingersVisible = value;
#endif
            }
        }

#if DEBUG
        private MockTouch _mockTouch;
#endif

        private bool _isMockActive;
        public bool IsMockActive
        {
            get
            {
                return _isMockActive;
            }
            set
            {
#if DEBUG
                _isMockActive = value;
#endif
            }
        }

        public void Reset()
        {
            _processor.Reset();

#if DEBUG
            _fingerMarkers.Clear();
#endif
        }

        void TouchFrameReported(object sender, TouchFrameEventArgs e)
        {
            if (!AssociatedObject.DesiredSize.Equals(new Size(0, 0))) //Code Fix by Devix: enables Multi-Page support
            {
                _processor.Process(e.GetTouchPoints(AssociatedObject));

#if DEBUG
                HandleDebugInfoAndFingers(
                    e.GetTouchPoints(RootVisual),
                    e.GetPrimaryTouchPoint(RootVisual));

#endif
            }
        }

        private bool RaiseManipulationStarted()
        {
            if (ManipulationStarted != null)
            {
                TryAttachToRoot();

                ManipulationStarted(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        private void TryAttachToRoot()
        {
            if (_rootVisual == null)
            {
                _rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
                _rootVisual.MouseLeftButtonUp += RootVisualMouseLeftButtonUp;
            }
        }

        private void RaiseManipulationCompleted()
        {
            if (ManipulationCompleted != null)
            {
                ManipulationCompleted(this, EventArgs.Empty);
            }
        }
    }
}
