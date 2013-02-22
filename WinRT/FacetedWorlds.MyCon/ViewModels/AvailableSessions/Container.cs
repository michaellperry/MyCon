using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.AvailableSessions
{
    public static class Container
    {
        public static AvailableSessionsViewModel CreateViewModel(
            Time time,
            Individual individual,
            SelectionModel selectionModel)
        {
            Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel = sessionPlace =>
                new SessionHeaderViewModel(sessionPlace, selectionModel);

            return new AvailableSessionsViewModel(time, newSessionHeaderViewModel);
        }
    }
}
