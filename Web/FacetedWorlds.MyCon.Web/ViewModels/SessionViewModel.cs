using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SessionViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public SessionViewModel(SessionPlace sessionPlace)
        {
            _sessionPlace = sessionPlace;
        }

        public string Name
        {
            get { return _sessionPlace.Session.Name; }
        }

        public string Day
        {
            get { return string.Format("{0:ddd}", _sessionPlace.Place.PlaceTime.Start); }
        }

        public string Time
        {
            get { return string.Format("{0:h:mm}", _sessionPlace.Place.PlaceTime.Start); }
        }

        public string Track
        {
            get { return _sessionPlace.Session.Track.Name; }
        }

        public string Room
        {
            get { return _sessionPlace.Place.Room.RoomNumber; }
        }

        public string Description
        {
            get
            {
                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Description.Value;
                if (segments == null)
                    return string.Empty;

                return string.Join("", segments.Select(segment => segment.Text).ToArray());
            }
        }
    }
}
