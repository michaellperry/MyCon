using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleSlotViewModel : SessionViewModelBase
    {
        private readonly Slot _slot;
        private readonly Schedule _schedule;
        private readonly ImageCache _imageCache;

        private Dependent<SessionPlace> _sessionPlace;

        public ScheduleSlotViewModel(Slot slot, Schedule schedule, ImageCache imageCache)
        {
            _slot = slot;
            _schedule = schedule;
            _imageCache = imageCache;

            _sessionPlace = new Dependent<SessionPlace>(() => SessionPlace);
        }

        public string Time
        {
            get { return String.Format("{0:h:mm}", _slot.SlotTime.Start); }
        }

        public CachedImage ImageUrl
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null)
                    return _imageCache.SmallImageUrl(sessionPlace.Session.Speaker.ImageUrl);
                else
                    return new CachedImage { ImageUrl = "/Images/unknown.small.png" };
            }
        }

        public string Title
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null)
                    return sessionPlace.Session.Name;
                else
                    return "Breakout Session";
            }
        }

        public string Speaker
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null)
                    return sessionPlace.Session.Speaker.Name;
                else
                    return "Tap for choices";
            }
        }

        public string Room
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null)
                    return sessionPlace.Place.Room.RoomNumber;
                else
                    return String.Empty;
            }
        }

        public bool Scheduled
        {
            get { return false; }
        }

        public bool Overbooked
        {
            get { return _slot.CurrentSchedules.Count() > 1; }
        }

        public override string TargetUri
        {
            get
            {
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                    return String.Format("/Views/SessionDetailsView.xaml?SessionId={0}", sessionPlace.Session.Id);
                else
                    return String.Format("/Views/SlotView.xaml?StartTime={0}", _slot.SlotTime.Start);
            }
        }

        private SessionPlace SessionPlace
        {
            get
            {
                if (_schedule != null)
                {
                    return _schedule.SessionPlace;
                }
                else
                {
                    List<SessionPlace> availableSessions = _slot.SlotTime.AvailableSessions.ToList();
                    if (availableSessions.Count == 1)
                    {
                        SessionPlace sessionPlace = availableSessions[0];
                        if (sessionPlace.Session.Track == null)
                            return sessionPlace;
                    }
                }
                return null;
            }
        }
    }
}
