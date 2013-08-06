using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.ImageUtilities;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public class TrackSessionViewModel
    {
        private readonly SessionSlot _sessionSlot;
        private readonly ImageCache _imageCache;

        public TrackSessionViewModel(SessionSlot sessionSlot, ImageCache imageCache)
        {
            _sessionSlot = sessionSlot;
            _imageCache = imageCache;
        }

        public DateTime Time
        {
            get { return _sessionSlot.Slot.SlotTime.StartTime.Value.ToLocalTime(); }
        }

        public CachedImage ImageUrl
        {
            get { return _imageCache.SmallImageUrl(_sessionSlot.Session.Speaker.ImageUrl); }
        }

        public string Title
        {
            get { return _sessionSlot.Session.Title; }
        }

        public string Speaker
        {
            get { return _sessionSlot.Session.Speaker.Name; }
        }

        public string Room
        {
            get { return _sessionSlot.Slot.Room.RoomNumber; }
        }

        public string Track
        {
            get
            {
                var track = _sessionSlot.Session.Tracks.FirstOrDefault();
                if (track == null)
                    return null;

                return track.Name;
            }
        }

        public bool Scheduled
        {
            get { return false; }
        }

        public bool Overbooked
        {
            get { return false; }
        }

        //public override string TargetUri
        //{
        //    get { return String.Format("/Views/SessionDetailsView.xaml?SessionId={0}", _sessionPlace.Session.Unique); }
        //}
    }
}
