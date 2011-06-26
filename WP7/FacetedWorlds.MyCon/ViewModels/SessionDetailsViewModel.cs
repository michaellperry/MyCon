using System;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using System.Linq;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionDetailsViewModel
    {
        private readonly Slot _slot;
        private readonly SessionPlace _sessionPlace;
        private readonly ImageCache _imageCache;

        public SessionDetailsViewModel(Slot slot, SessionPlace sessionPlace, ImageCache imageCache)
        {
            _slot = slot;
            _sessionPlace = sessionPlace;
            _imageCache = imageCache;
        }

        public string Time
        {
            get
            {
                return String.Format(
                    "{0:dddd, MMMM d} at {1:h:mm tt}",
                    _sessionPlace.Place.PlaceTime.Day.ConferenceDate,
                    _sessionPlace.Place.PlaceTime.Start);
            }
        }

        public string ImageUrl
        {
            get { return _imageCache.LargeImageUrl(_sessionPlace.Session.Speaker.ImageUrl); }
        }

        public string Title
        {
            get { return _sessionPlace.Session.Name; }
        }

        public string Speaker
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public string Room
        {
            get { return _sessionPlace.Place.Room.RoomNumber; }
        }

        public string Track
        {
            get
            {
                Track track = _sessionPlace.Session.Track;
                return track == null ? String.Empty : String.Format("Track: {0}", track.Name);
            }
        }

        public string Level
        {
            get
            {
                Level level = _sessionPlace.Session.Level.Value;
                return level == null ? String.Empty : String.Format("Level: {0}", level.Name);
            }
        }

        public string Description
        {
            get { return JoinSegments(_sessionPlace.Session.Description.Value); }
        }

        public string Contact
        {
            get { return _sessionPlace.Session.Speaker.Contact; }
        }

        public string Bio
        {
            get { return JoinSegments(_sessionPlace.Session.Speaker.Bio.Value); }
        }

        private static string JoinSegments(IEnumerable<DocumentSegment> segments)
        {
            if (segments == null)
                return null;
            return String.Join("", segments
                .Select(segment => segment.Text)
                .ToArray());
        }
    }
}
