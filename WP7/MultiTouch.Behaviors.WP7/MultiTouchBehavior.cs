// ****************************************************************************
// <copyright file="MultiTouchManipulationBehavior.cs" company="Laurent Bugnion">
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
// <LastBaseLevel>BL0001</LastBaseLevel>
// ****************************************************************************

using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace MultiTouch.Behaviors.WP7
{
    /// <summary>
    /// Implements Multi-Touch Manipulation
    /// </summary>
    public partial class MultiTouchBehavior : Behavior<FrameworkElement>
    {
        public event EventHandler<EventArgs> ManipulationStarted;

        public event EventHandler<EventArgs> ManipulationCompleted;

#if !WINDOWS_PHONE
        /// <summary>
        /// The <see cref="IsInertiaEnabled" /> dependency property's name.
        /// </summary>
        public const string IsInertiaEnabledPropertyName = "IsInertiaEnabled";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsInertiaEnabled" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsInertiaEnabled
        {
            get
            {
                return (bool)GetValue(IsInertiaEnabledProperty);
            }
            set
            {
                SetValue(IsInertiaEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsInertiaEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.Register(
            IsInertiaEnabledPropertyName,
            typeof(bool),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(true, OnIsInertiaEnabledChanged));
#endif

        /// <summary>
        /// The <see cref="IsScaleEnabled" /> dependency property's name.
        /// </summary>
        public const string IsScaleEnabledPropertyName = "IsScaleEnabled";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsScaleEnabled" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsScaleEnabled
        {
            get
            {
                return (bool)GetValue(IsScaleEnabledProperty);
            }
            set
            {
                SetValue(IsScaleEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsScaleEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsScaleEnabledProperty = DependencyProperty.Register(
            IsScaleEnabledPropertyName,
            typeof(bool),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(true, OnIsScaleEnabledChanged));

        /// <summary>
        /// The <see cref="IsRotateEnabled" /> dependency property's name.
        /// </summary>
        public const string IsRotateEnabledPropertyName = "IsRotateEnabled";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsRotateEnabled" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsRotateEnabled
        {
            get
            {
                return (bool)GetValue(IsRotateEnabledProperty);
            }
            set
            {
                SetValue(IsRotateEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsRotateEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsRotateEnabledProperty = DependencyProperty.Register(
            IsRotateEnabledPropertyName,
            typeof(bool),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(true, OnIsRotateEnabledChanged));

        /// <summary>
        /// The <see cref="IsTranslateXEnabled" /> dependency property's name.
        /// </summary>
        public const string IsTranslateXEnabledPropertyName = "IsTranslateXEnabled";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsTranslateXEnabled" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsTranslateXEnabled
        {
            get
            {
                return (bool)GetValue(IsTranslateXEnabledProperty);
            }
            set
            {
                SetValue(IsTranslateXEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsTranslateXEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTranslateXEnabledProperty = DependencyProperty.Register(
            IsTranslateXEnabledPropertyName,
            typeof(bool),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(true, OnIsTranslateXEnabledChanged));

        /// <summary>
        /// The <see cref="IsTranslateYEnabled" /> dependency property's name.
        /// </summary>
        public const string IsTranslateYEnabledPropertyName = "IsTranslateYEnabled";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsTranslateYEnabled" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsTranslateYEnabled
        {
            get
            {
                return (bool)GetValue(IsTranslateYEnabledProperty);
            }
            set
            {
                SetValue(IsTranslateYEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsTranslateYEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTranslateYEnabledProperty = DependencyProperty.Register(
            IsTranslateYEnabledPropertyName,
            typeof(bool),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(true, OnIsTranslateYEnabledChanged));

        /// <summary>
        /// The <see cref="MinimumScale" /> dependency property's name.
        /// </summary>
        public const string MinimumScalePropertyName = "MinimumScale";

        /// <summary>
        /// Gets or sets the value of the <see cref="MinimumScale" />
        /// property. This is a dependency property.
        /// </summary>
        public double MinimumScale
        {
            get
            {
                return (double)GetValue(MinimumScaleProperty);
            }
            set
            {
                SetValue(MinimumScaleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="MinimumScale" /> dependency property.
        /// TODO Should coerce with MaximumScale
        /// </summary>
        public static readonly DependencyProperty MinimumScaleProperty = DependencyProperty.Register(
            MinimumScalePropertyName,
            typeof(double),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(0.5, OnMinimumScaleChanged));

        /// <summary>
        /// The <see cref="MaximumScale" /> dependency property's name.
        /// </summary>
        public const string MaximumScalePropertyName = "MaximumScale";

        /// <summary>
        /// Gets or sets the value of the <see cref="MaximumScale" />
        /// property. This is a dependency property.
        /// </summary>
        public double MaximumScale
        {
            get
            {
                return (double)GetValue(MaximumScaleProperty);
            }
            set
            {
                SetValue(MaximumScaleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="MaximumScale" /> dependency property.
        /// TODO Should coerce with MinimumScale
        /// </summary>
        public static readonly DependencyProperty MaximumScaleProperty = DependencyProperty.Register(
            MaximumScalePropertyName,
            typeof(double),
            typeof(MultiTouchBehavior),
            new PropertyMetadata(100.0, OnMaximumScaleChanged));

        /// <summary>
        /// Initialize the behavior
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            var existingTransform = AssociatedObject.RenderTransform as MatrixTransform;
            if (existingTransform == null
                || existingTransform.Matrix != Matrix.Identity)
            {
                throw new InvalidOperationException("Cannot attach to an element with an existing transform");
            }

            OnAttachedImpl();
        }

        /// <summary>
        /// Occurs when detaching the behavior
        /// </summary>
        protected override void OnDetaching()
        {
            OnDetachingImpl();
            AssociatedObject.RenderTransform = null;
            base.OnDetaching();
        }
    }
}
