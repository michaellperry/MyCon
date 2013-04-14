using System;
using System.Linq;
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

        public DateTime Time
        {
            get { return _slot.SlotTime.Start.ToLocalTime(); }
        }

        public CachedImage ImageUrl
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

        public string Track
        {
            get
            {
                return _sessionPlace.Session.Track.IsNull
                    ? String.Empty
                    : _sessionPlace.Session.Track.Name;
            }
        }

        public bool Scheduled
        {
            get { return _slot.IsScheduled(_sessionPlace); }
        }

        public bool Overbooked
        {
            get { return Scheduled && _slot.CurrentSchedules.Count() > 1; }
        }

        public override string TargetUri
        {
            get { return String.Format("/Views/SessionDetailsView.xaml?SessionId={0}", _sessionPlace.Session.Unique); }
        }
    }
}
