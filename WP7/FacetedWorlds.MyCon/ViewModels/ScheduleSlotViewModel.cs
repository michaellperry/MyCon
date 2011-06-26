using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using Microsoft.Phone.Controls;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleSlotViewModel : SessionViewModelBase
    {
        private readonly Slot _slot;
        private readonly Schedule _schedule;
        private readonly ImageCache _imageCache;

        public ScheduleSlotViewModel(Slot slot, Schedule schedule, ImageCache imageCache)
        {
            _slot = slot;
            _schedule = schedule;
            _imageCache = imageCache;
        }

        public string Time
        {
            get { return String.Format("{0:h:mm tt}", _slot.SlotTime.Start); }
        }

        public string ImageUrl
        {
            get
            {
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                    return _imageCache.SmallImageUrl(sessionPlace.Session.Speaker.ImageUrl);
                else
                    return "/Images/appbar.favs.rest.png";
            }
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

        public override string TargetUri
        {
            get
            {
                SessionPlace sessionPlace = SessionPlace;
                if (sessionPlace != null)
                    return String.Format("/Views/SessionDetailsView.xaml?SessionId={0}", sessionPlace.Session.Id);
                else
                    return null;
            }
        }
    }
}
