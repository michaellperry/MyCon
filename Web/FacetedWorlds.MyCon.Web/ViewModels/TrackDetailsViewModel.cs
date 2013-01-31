using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class TrackDetailsViewModel
    {
        private readonly Track _track;

        public TrackDetailsViewModel(Track track)
        {
            _track = track;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _track.CurrentSessionPlaces
                    where sessionPlace.Place != null
                       && sessionPlace.Place.PlaceTime != null
                       && sessionPlace.Place.Room != null
                    orderby sessionPlace.Place.PlaceTime.Start, sessionPlace.Place.Room.RoomNumber.Ensure().Value
                    select new SessionViewModel(sessionPlace);
            }
        }
    }
}
