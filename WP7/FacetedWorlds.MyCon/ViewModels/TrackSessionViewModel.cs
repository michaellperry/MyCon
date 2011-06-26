using System;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TrackSessionViewModel : SessionViewModelBase
    {
        private readonly Slot _slot;
        private readonly SessionPlace _sessionPlace;
        private readonly ImageCache _imageCache;

        public TrackSessionViewModel(Slot slot, SessionPlace sessionPlace, ImageCache imageCache)
        {
            _slot = slot;
            _sessionPlace = sessionPlace;
            _imageCache = imageCache;
        }

        public string Time
        {
            get { return String.Format("{0:h:mm tt}", _slot.SlotTime.Start); }
        }

        public string ImageUrl
        {
            get { return _imageCache.SmallImageUrl(_sessionPlace.Session.Speaker.ImageUrl); }
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

        public override string TargetUri
        {
            get { return String.Format("/Views/SessionDetailsView.xaml?SessionId={0}", _sessionPlace.Session.Id); }
        }
    }
}
