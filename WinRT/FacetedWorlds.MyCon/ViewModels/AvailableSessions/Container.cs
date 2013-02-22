using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FacetedWorlds.MyCon.ViewModels.AvailableSessions
{
    public static class Container
    {
        public static AvailableSessionsViewModel CreateViewModel(
            Time time,
            Individual individual,
            SelectionModel selectionModel)
        {
            var frame = Window.Current.Content as Frame;

            Action showSession = () =>
            {
                frame.Navigate(typeof(SessionView));
            };

            Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel = sessionPlace =>
                new SessionHeaderViewModel(sessionPlace, individual, selectionModel, showSession);

            return new AvailableSessionsViewModel(time, newSessionHeaderViewModel);
        }
    }
}
