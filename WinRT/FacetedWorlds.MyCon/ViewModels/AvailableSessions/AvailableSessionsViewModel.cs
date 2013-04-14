using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.AvailableSessions
{
    public class AvailableSessionsViewModel
    {
        private readonly Time _time;
        private readonly Func<SessionPlace, SessionHeaderViewModel> _newSessionHeaderViewModel;
        
        public AvailableSessionsViewModel(Time time, Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel)
        {
            _time = time;
            _newSessionHeaderViewModel = newSessionHeaderViewModel;
        }

        public IEnumerable<SessionHeaderViewModel> Sessions
        {
            get
            {
                return
                    from session in _time.AvailableSessions
                    orderby session.Place.Room.RoomNumber.Value
                    select _newSessionHeaderViewModel(session);
            }
        }
    }
}
