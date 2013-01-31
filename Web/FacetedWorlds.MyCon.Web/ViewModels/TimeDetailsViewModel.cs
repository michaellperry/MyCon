using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Web.Extensions;

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
            get { return string.Format("{0:dddd MMMM d h:mm}", _time.Start.ConvertTo("Central Standard Time")); }
        }

        public IEnumerable<SessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _time.AvailableSessions
                    where sessionPlace.Session != null
                    orderby sessionPlace.Session.Name.Ensure().Value
                    select new SessionViewModel(sessionPlace);
            }
        }
    }
}