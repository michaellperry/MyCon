using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;
using UpdateControls.XAML;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class ScheduleSlotViewModel
    {
        private readonly Time _time;
        private readonly Schedule _schedule;

        private Dependent<SessionPlace> _sessionPlace;
        private readonly Individual _individual;
        private readonly Action<Time> _showTime;
        
        public ScheduleSlotViewModel(Time time, Individual individual, Schedule schedule, Action<Time> showTime)
        {
            _time = time;
            _individual = individual;
            _schedule = schedule;
            _showTime = showTime;

            _sessionPlace = new Dependent<SessionPlace>(() => SessionPlace);
        }

        public ImageSource Image
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace == null ||
                    sessionPlace.Session == null ||
                    sessionPlace.Session.Speaker == null)
                    return null;

                string url = sessionPlace.Session.Speaker.ImageUrl;
                if (String.IsNullOrWhiteSpace(url))
                    return null;

                return new BitmapImage(new Uri(url, UriKind.Absolute));
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

        public ICommand SelectSlot
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _showTime(_time);
                    });
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
