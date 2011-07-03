using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace MultiTouch.Behaviors.WP7
{
    /// <summary>
    /// Implements Multi-Touch Manipulation
    /// </summary>
    public class MultiTouchManipulationBehavior : Behavior<FrameworkElement>
    {
        private ScaleTransform _scaleTransform;
        private TranslateTransform _translateTransform;

        /// <summary>
        /// Initialize the behavior
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            var tg=new TransformGroup();
            _scaleTransform=new ScaleTransform(); _translateTransform=new TranslateTransform();
            tg.Children.Add(_scaleTransform); tg.Children.Add(_translateTransform);
            AssociatedObject.RenderTransform = tg;

            AssociatedObject.ManipulationDelta += (s1, e) =>
              {
                  {
                      // Scale
                      if (e.DeltaManipulation.Scale.X != 0)
                          _scaleTransform.ScaleX *= e.DeltaManipulation.Scale.X;
                      if (e.DeltaManipulation.Scale.Y != 0)
                          _scaleTransform.ScaleY *= e.DeltaManipulation.Scale.Y;

                      // Translation
                      _translateTransform.X += e.DeltaManipulation.Translation.X;
                      _translateTransform.Y += e.DeltaManipulation.Translation.Y;
                  }
              };
        }

        /// <summary>
        /// Occurs when detaching the behavior
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
