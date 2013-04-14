using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;
using UpdateControls.XAML;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using FacetedWorlds.MyCon.Models;
using Windows.UI.Xaml;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class ScheduleSlotViewModel
    {
        private readonly Time _time;
        private readonly Schedule _schedule;

        private Dependent<SessionPlace> _sessionPlace;
        private readonly Individual _individual;
        private readonly SelectionModel _selection;
        
        public ScheduleSlotViewModel(Time time, Individual individual, Schedule schedule, SelectionModel selection)
        {
            _time = time;
            _individual = individual;
            _schedule = schedule;
            _selection = selection;

            _sessionPlace = new Dependent<SessionPlace>(() => SessionPlace);
        }

        public ImageSource Image
        {
            get
            {
                SessionPlace sessionPlace = _sessionPlace;
                if (sessionPlace == null)
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
                    return "Room: " + sessionPlace.Place.Room.RoomNumber.Value;
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
                        _selection.SelectedTime =
                            _selection.SelectedTime == _time
                                ? null
                                : _time;
                    });
            }
        }

        public bool IsSelected
        {
            get { return _selection.SelectedTime == _time; }
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
                        if (sessionPlace.Session.Track.IsNull)
                            return sessionPlace;
                    }
                }
                return null;
            }
        }
    }
}
