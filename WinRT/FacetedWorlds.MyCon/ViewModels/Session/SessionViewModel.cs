using System;
using System.Collections.Generic;
using FacetedWorlds.MyCon.Model;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.Session
{
    public class SessionViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public SessionViewModel(SessionPlace sessionPlace)
        {
            _sessionPlace = sessionPlace;
        }

        public string Title
        {
            get
            {
                if (_sessionPlace.Session == null)
                    return null;

                return _sessionPlace.Session.Name;
            }
        }

        public ImageSource Image
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                string url = _sessionPlace.Session.Speaker.ImageUrl;
                if (String.IsNullOrWhiteSpace(url))
                    return null;

                return new BitmapImage(new Uri(url, UriKind.Absolute));
            }
        }

        public string Day
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.PlaceTime == null)
                    return null;

                DateTime start = _sessionPlace.Place.PlaceTime.Start;
                return start.ToString("ddd");
            }
        }

        public string Time
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.PlaceTime == null)
                    return null;

                DateTime start = _sessionPlace.Place.PlaceTime.Start;
                return start.ToString("h:mm");
            }
        }

        public string Room
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.Room == null)
                    return null;

                return String.Format("Room {0}", _sessionPlace.Place.Room.RoomNumber.Value);
            }
        }

        public string Track
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Track == null)
                    return null;

                return String.Format("Track: {0}", _sessionPlace.Session.Track.Name);
            }
        }

        public string Description
        {
            get
            {
                if (_sessionPlace.Session == null)
                    return null;

                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Description.Value;
                if (segments == null)
                    return null;

                return segments.JoinSegments();
            }
        }

        public string Speaker
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                return _sessionPlace.Session.Speaker.Name;
            }
        }

        public string SpeakerBio
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Speaker.Bio.Value;
                if (segments == null)
                    return null;

                return segments.JoinSegments();
            }
        }
    }
}
