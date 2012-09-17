using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class TimeDetailsViewModel
    {
        private readonly Time _time;

        public TimeDetailsViewModel(Time time)
        {
            _time = time;
        }

        public string Time
        {
            get { return string.Format("{0:dddd MMMM d h:mm}", _time.Start.ToLocalTime()); }
        }

        public IEnumerable<SessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _time.AvailableSessions
                    orderby sessionPlace.Session.Name.Value
                    select new SessionViewModel(sessionPlace);
            }
        }
    }
}