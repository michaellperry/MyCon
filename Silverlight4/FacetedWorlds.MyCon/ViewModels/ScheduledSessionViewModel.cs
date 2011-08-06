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

        public void MoveTo(Place place)
        {
            _sessionPlace.Session.SetPlace(place);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduledSessionViewModel that = obj as ScheduledSessionViewModel;
            if (that == null)
                return false;
            return _sessionPlace.Equals(that._sessionPlace);
        }

        public override int GetHashCode()
        {
            return _sessionPlace.GetHashCode();
        }
    }
}
