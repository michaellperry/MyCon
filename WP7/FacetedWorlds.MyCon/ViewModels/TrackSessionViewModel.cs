using System;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TrackSessionViewModel
    {
        private readonly Slot _slot;
        private readonly SessionPlace _sessionPlace;

        public TrackSessionViewModel(Slot slot, SessionPlace sessionPlace)
        {
            _slot = slot;
            _sessionPlace = sessionPlace;
        }

        public string Time
        {
            get { return String.Format("{0:h:mm tt}", _slot.SlotTime.Start); }
        }

        public string ImageUrl
        {
            get { return "http://a3.twimg.com/profile_images/105305569/IMG_0147.jpg"; }
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
    }
}
