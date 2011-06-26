using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TrackDayViewModel
    {
        private readonly Attendee _attendee;
        private readonly Track _track;
        private readonly Day _day;
        private readonly ImageCache _imageCache;

        public TrackDayViewModel(Attendee attendee, Track track, Day day, ImageCache imageCache)
        {
            _attendee = attendee;
            _track = track;
            _day = day;
            _imageCache = imageCache;
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _day.ConferenceDate.Date); }
        }

        public IEnumerable<TrackSessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _track.CurrentSessionPlaces
                    where sessionPlace.Place.PlaceTime.Day == _day
                    select new TrackSessionViewModel(_attendee.NewSlot(sessionPlace.Place.PlaceTime), sessionPlace, _imageCache);
            }
        }
    }
}
