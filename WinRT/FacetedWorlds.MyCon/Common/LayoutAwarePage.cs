using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;

namespace FacetedWorlds.MyCon.Common
{
    public abstract class LayoutAwarePage : Page, ILayoutAwareControl
    {
        private List<ILayoutAwareControl> _layoutAwareControls;

        public LayoutAwarePage()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

            this.Loaded += (sender, e) =>
            {
                this.StartLayoutUpdates(this);
            };

            this.Unloaded += (sender, e) =>
            {
                this.StopLayoutUpdates(this);
            };
        }

        public abstract void SetLayout(ApplicationViewState viewState);

        public void StartLayoutUpdates(ILayoutAwareControl control)
        {
            if (control == null) return;
            if (this._layoutAwareControls == null)
            {
                // Start listening to view state changes when there are controls interested in updates
                Window.Current.SizeChanged += this.WindowSizeChanged;
                this._layoutAwareControls = new List<ILayoutAwareControl>();
            }
            this._layoutAwareControls.Add(control);

            // Set the initial visual state of the control
            control.SetLayout(ApplicationView.Value);
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }

        public void StopLayoutUpdates(ILayoutAwareControl control)
        {
            if (control == null || this._layoutAwareControls == null) return;
            this._layoutAwareControls.Remove(control);
            if (this._layoutAwareControls.Count == 0)
            {
                // Stop listening to view state changes when no controls are interested in updates
                this._layoutAwareControls = null;
                Window.Current.SizeChanged -= this.WindowSizeChanged;
            }
        }

        public void InvalidateVisualState()
        {
            if (this._layoutAwareControls != null)
            {
                foreach (var layoutAwareControl in this._layoutAwareControls)
                {
                    layoutAwareControl.SetLayout(ApplicationView.Value);
                }
            }
        }
    }
}
