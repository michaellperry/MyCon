using System.Windows;
using UpdateControls;
using System;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.Schedule.Views
{
    public static class ViewExtensions
    {
        public static Dependent UpdateWhenNecessary(this DependencyObject dependencyObject, Action update)
        {
            Dependent dependent = new Dependent(update);
            dependent.Invalidated += () => dependencyObject.Dispatcher.BeginInvoke(() => dependent.OnGet());
            dependent.OnGet();
            return dependent;
        }

        public static ApplicationBarIconButton Button(this PhoneApplicationPage page, int index)
        {
            return (ApplicationBarIconButton)(page.ApplicationBar).Buttons[index];
        }

        public static ApplicationBarMenuItem MenuItem(this PhoneApplicationPage page, int index)
        {
            return (ApplicationBarMenuItem)(page.ApplicationBar).MenuItems[index];
        }
    }
}
