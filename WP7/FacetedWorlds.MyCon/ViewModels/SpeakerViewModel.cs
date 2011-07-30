using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SpeakerViewModel
    {
        private readonly Attendee _attendee;
        private readonly Speaker _speaker;
        private readonly ImageCache _imageCache;

        public SpeakerViewModel(Attendee attendee, Speaker speaker, ImageCache imageCache)
        {
            _attendee = attendee;
            _speaker = speaker;
            _imageCache = imageCache;
        }

        public string Name
        {
            get { return _speaker.Name; }
        }

        public string Contact
        {
            get { return _speaker.Contact; }
        }

        public CachedImage ImageUrl
        {
            get { return _imageCache.LargeImageUrl(_speaker.ImageUrl); }
        }

        public IEnumerable<SpeakerDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new SpeakerDayViewModel(_attendee, _speaker, day, _imageCache);
            }
        }

        public string Bio
        {
            get { return _speaker.Bio.Value.JoinSegments(); }
        }
    }
}
