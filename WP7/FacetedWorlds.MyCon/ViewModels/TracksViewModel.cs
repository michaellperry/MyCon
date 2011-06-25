using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TracksViewModel
    {
        private readonly Attendee _attendee;

        public TracksViewModel(Attendee attendee)
        {
            _attendee = attendee;
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _attendee.Conference.Tracks
                    orderby track.Name
                    select new TrackViewModel(_attendee, track);
            }
        }
    }
}
