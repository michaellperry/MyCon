using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleSlotViewModel : SessionViewModelBase
    {
        private readonly Time _time;
        private readonly Attendee _attendee;
        private readonly Schedule _schedule;

        private Dependent<SessionPlace> _sessionPlace;
        
        public ScheduleSlotViewModel(Time time, Attendee attendee, Schedule schedule)
        {
            _time = time;
            _attendee = attendee;
            _schedule = schedule;

            _sessionPlace = new Dependent<SessionPlace>(() => SessionPlace);
        }

        public string Time
        {
            get { return String.Format("{0:h:mm}", _time.Start); }
        }

        public string ImageUrl
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null &&
                    sessionPlace.Session != null &&
                    sessionPlace.Session.Speaker != null)
                    return sessionPlace.Session.Speaker.ImageUrl;
                else
                    return "/Images/unknown.small.png";
            }
        }

        public string Title
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace != null &&
                    sessionPlace.Session != null)
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
                if (sessionPlace != null &&
                    sessionPlace.Session != null &&
                    sessionPlace.Session.Speaker != null)
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
                if (sessionPlace != null &&
                    sessionPlace.Place != null &&
                    sessionPlace.Place.Room != null)
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
            get
            {
                return
                    _schedule != null &&
                    _schedule.Slot != null &&
                    _schedule.Slot.CurrentSchedules.Count() > 1;
            }
        }

        public override string TargetUri
        {
            get
            {
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                {
                    if (sessionPlace.Session != null)
                        return String.Format(
                            "/Views/SessionDetailsView.xaml?SessionId={0}",
                            sessionPlace.Session.Unique);
                    else
                        return String.Empty;
                }
                else
                {
                    return String.Format("/Views/SlotView.xaml?StartTime={0}",
                        _time.Start);
                }
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
                    List<SessionPlace> availableSessions = _time.AvailableSessions.ToList();
                    if (availableSessions.Count == 1)
                    {
                        SessionPlace sessionPlace = availableSessions[0];
                        if (sessionPlace.Session != null &&
                            sessionPlace.Session.Track == null)
                            return sessionPlace;
                    }
                }
                return null;
            }
        }
    }
}
