using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TrackViewModel
    {
        private readonly Attendee _attendee;
        private readonly Track _track;
        private readonly ImageCache _imageCache;

        public TrackViewModel(Attendee attendee, Track track, ImageCache imageCache)
        {
            _attendee = attendee;
            _track = track;
            _imageCache = imageCache;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<TrackDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new TrackDayViewModel(_attendee, _track, day, _imageCache);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
