using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduledSessionViewModel
    {
        private readonly Session _session;

        public ScheduledSessionViewModel(Session session)
        {
            _session = session;
        }

        public string SessionName
        {
            get { return _session.Name; }
        }

        public string Speaker
        {
            get { return _session.Speaker.Name; }
        }

        public string Track
        {
            get
            {
                return _session.Track == null
                    ? string.Empty :
                    _session.Track.Name;
            }
        }

        public void MoveTo(Place place)
        {
            _session.SetPlace(place);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduledSessionViewModel that = obj as ScheduledSessionViewModel;
            if (that == null)
                return false;
            return _session.Equals(that._session);
        }

        public override int GetHashCode()
        {
            return _session.GetHashCode();
        }
    }
}
