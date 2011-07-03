using System;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MapViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        
        public MapViewModel(Attendee attendee, ImageCache imageCache)
        {
            _attendee = attendee;
            _imageCache = imageCache;
        }

        public string NextSession
        {
            get
            {
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
                        place.PlaceTime.Start < CurrentTime.AddMinutes(60.0) &&
                        place.PlaceTime.Start > CurrentTime.AddMinutes(-30.0)
                    orderby place.PlaceTime.Start
                    select place;
                var nextPlace = currentPlaces.FirstOrDefault();
                if (nextPlace != null)
                {
                    int minutes = (nextPlace.PlaceTime.Start - CurrentTime).Minutes;
                    if (minutes > 0)
                    {
                        return String.Format("Your next session in {0} in {1} minutes",
                            nextPlace.Room.RoomNumber,
                            minutes);
                    }
                    else
                    {
                        return String.Format("Your next session in {0} {1} minutes ago",
                            nextPlace.Room.RoomNumber,
                            -minutes);
                    }
                }

                return null;
            }
        }

        public string MapImageUrl
        {
            get { return _imageCache.OriginalImageUrl(_attendee.Conference.MapUrl.Value); }
        }

        private DateTime CurrentTime
        {
            get
            {
                return new DateTime(2009, 11, 6, 11, 52, 45);
            }
        }
    }
}
