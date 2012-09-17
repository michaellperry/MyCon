using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Web.Extensions;

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
            get { return string.Format("{0:yyyyMMddHHmm}", _time.Start.ConvertTo("Central Standard Time")); }
        }

        public string Day
        {
            get { return string.Format("{0:ddd}", _time.Start.ConvertTo("Central Standard Time")); }
        }

        public string Time
        {
            get { return string.Format("{0:h:mm}", _time.Start.ConvertTo("Central Standard Time")); }
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
