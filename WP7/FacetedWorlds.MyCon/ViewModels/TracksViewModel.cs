using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TracksViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;

        public TracksViewModel(Attendee attendee, ImageCache imageCache)
        {
            _attendee = attendee;
            _imageCache = imageCache;
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _attendee.Conference.Tracks
                    orderby track.Name
                    select new TrackViewModel(_attendee, track, _imageCache);
            }
        }
    }
}
