using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleSlotViewModel
    {
        private readonly Slot _slot;
        private readonly Schedule _schedule;

        public ScheduleSlotViewModel(Slot slot, Schedule schedule)
        {
            _slot = slot;
            _schedule = schedule;
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
            get
            {
                SessionPlace sessionPlace = SessionPlace;
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
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                    return sessionPlace.Session.Speaker.Name;
                else
                    return String.Empty;
            }
        }

        public string Room
        {
            get
            {
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                    return sessionPlace.Place.Room.RoomNumber;
                else
                    return String.Empty;
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
                        return availableSessions.Single();
                    }
                }
                return null;
            }
        }
    }
}
