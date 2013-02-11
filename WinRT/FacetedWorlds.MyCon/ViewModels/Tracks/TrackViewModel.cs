using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class TrackViewModel
    {
        private readonly Track _track;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SessionHeaderViewModel> Items
        {
            get
            {
                var sessionPlaces = _track.CurrentSessionPlaces.ToList();
                return
                    from sessionPlace in sessionPlaces
                    where sessionPlace.Place != null
                       && sessionPlace.Place.PlaceTime != null
                    orderby sessionPlace.Place.PlaceTime.Start
                    select new SessionHeaderViewModel(sessionPlace);
            }
        }
    }
}
