using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class TimeViewModel
    {
        private readonly Time _time;

        public TimeViewModel(Time time)
        {
            _time = time;
        }

        public string ID
        {
            get { return string.Format("{0:yyyyMMddHHmm}", _time.Start.ToLocalTime()); }
        }

        public string Day
        {
            get { return string.Format("{0:ddd}", _time.Start.ToLocalTime()); }
        }

        public string Time
        {
            get { return string.Format("{0:h:mm}", _time.Start.ToLocalTime()); }
        }

        public SessionViewModel CommonSession
        {
            get
            {
                var sessions = _time.AvailableSessions
                    .Where(session => session.Session.Track == null)
                    .ToList();
                if (sessions.Count == 1)
                    return new SessionViewModel(sessions[0]);
                else
                    return null;
            }
        }
    }
}
