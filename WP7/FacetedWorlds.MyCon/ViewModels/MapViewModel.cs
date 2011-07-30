using System;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MapViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly Clock _clock;

        public MapViewModel(Attendee attendee, ImageCache imageCache, Clock clock)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _clock = clock;
        }

        public string NextSession
        {
            get
            {
                DateTime now = _clock.Time;

                var generalSessionPlaces =
                    from session in _attendee.Conference.Sessions
                    where session.Track == null
                    from sessionPlace in session.CurrentSessionPlaces
                    select sessionPlace.Place;
                var breakoutSessionPlaces =
                    from schedule in _attendee.CurrentSchedules
                    select schedule.SessionPlace.Place;
                var currentPlaces =
                    from place in generalSessionPlaces.Union(breakoutSessionPlaces)
                    where
                        place.PlaceTime.Start < now.AddMinutes(60.0) &&
                        place.PlaceTime.Start > now.AddMinutes(-30.0)
                    orderby place.PlaceTime.Start
                    select place;
                var nextPlace = currentPlaces.FirstOrDefault();
                if (nextPlace != null)
                {
                    int minutes = (int)(nextPlace.PlaceTime.Start - now).TotalMinutes;
                    if (minutes <= -2)
                    {
                        return String.Format("Your next session in {0} {1} minutes ago",
                            nextPlace.Room.RoomNumber,
                            -minutes);
                    }
                    else if (minutes <= -1)
                    {
                        return String.Format("Your next session in {0} a minute ago",
                            nextPlace.Room.RoomNumber);
                    }
                    else if (minutes <= 0)
                    {
                        return String.Format("Your next session in {0} now",
                            nextPlace.Room.RoomNumber);
                    }
                    else if (minutes <= 1)
                    {
                        return String.Format("Your next session in {0} a minute",
                            nextPlace.Room.RoomNumber);
                    }
                    else
                    {
                        return String.Format("Your next session in {0} in {1} minutes",
                            nextPlace.Room.RoomNumber,
                            minutes);
                    }
                }

                return null;
            }
        }

        public CachedImage MapImageUrl
        {
            get { return _imageCache.OriginalImageUrl(_attendee.Conference.MapUrl.Value); }
        }
    }
}
