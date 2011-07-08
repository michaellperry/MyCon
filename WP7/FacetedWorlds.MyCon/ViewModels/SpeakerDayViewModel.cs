using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Collections;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SpeakerDayViewModel
    {
        private readonly Attendee _attendee;
        private readonly Speaker _speaker;
        private readonly Day _day;
        private readonly ImageCache _imageCache;

        private DependentList<SessionPlace> _matchingSessionPlaces;

        public SpeakerDayViewModel(Attendee attendee, Speaker speaker, Day day, ImageCache imageCache)
        {
            _attendee = attendee;
            _speaker = speaker;
            _day = day;
            _imageCache = imageCache;

            _matchingSessionPlaces = new DependentList<SessionPlace>(() =>
                from sessionPlace in _speaker.AvailableSessions
                where sessionPlace.Place.PlaceTime.Day == _day
                select sessionPlace);
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _day.ConferenceDate.Date); }
        }

        public Visibility DayVisible
        {
            get { return _matchingSessionPlaces.Any() ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IEnumerable<TrackSessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _matchingSessionPlaces
                    orderby sessionPlace.Place.PlaceTime.Start
                    select new TrackSessionViewModel(
                        _attendee.NewSlot(sessionPlace.Place.PlaceTime),
                        sessionPlace,
                        _imageCache);
            }
        }
    }
}
