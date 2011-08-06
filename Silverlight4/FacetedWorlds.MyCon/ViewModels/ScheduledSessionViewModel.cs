using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduledSessionViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public ScheduledSessionViewModel(SessionPlace sessionPlace)
        {
            _sessionPlace = sessionPlace;
        }

        public string SessionName
        {
            get { return _sessionPlace.Session.Name; }
        }

        public string Speaker
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public string Track
        {
            get
            {
                return _sessionPlace.Session.Track == null
                    ? string.Empty :
                    _sessionPlace.Session.Track.Name;
            }
        }
    }
}
