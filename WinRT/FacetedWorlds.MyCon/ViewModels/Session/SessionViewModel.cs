using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using UpdateControls.XAML;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.Session
{
    public class SessionViewModel
    {
        private readonly SessionPlace _sessionPlace;
        private readonly Individual _individual;

        public SessionViewModel(SessionPlace sessionPlace, Individual individual)
        {
            _sessionPlace = sessionPlace;
            _individual = individual;
        }

        public string Title
        {
            get { return _sessionPlace.Session.Name; }
        }

        public ImageSource Image
        {
            get
            {
                string url = _sessionPlace.Session.Speaker.ImageUrl;
                if (String.IsNullOrWhiteSpace(url))
                    return null;

                return new BitmapImage(new Uri(url, UriKind.Absolute));
            }
        }

        public string Day
        {
            get
            {
                DateTime start = _sessionPlace.Place.PlaceTime.Start.ToLocalTime();
                return start.ToString("ddd");
            }
        }

        public string Time
        {
            get
            {
                DateTime start = _sessionPlace.Place.PlaceTime.Start.ToLocalTime();
                return start.ToString("h:mm");
            }
        }

        public string Room
        {
            get { return String.Format("Room: {0}", _sessionPlace.Place.Room.RoomNumber.Value); }
        }

        public string Track
        {
            get
            {
                if (_sessionPlace.Session.Track.IsNull)
                    return null;

                return String.Format("Track: {0}", _sessionPlace.Session.Track.Name);
            }
        }

        public string Description
        {
            get
            {
                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Description.Value;
                return segments.JoinSegments();
            }
        }

        public string Speaker
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public string SpeakerBio
        {
            get
            {
                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Speaker.Bio.Value;
                return segments.JoinSegments();
            }
        }

        public Visibility AddVisible
        {
            get { return _individual.IsScheduled(_sessionPlace) ? Visibility.Collapsed : Visibility.Visible; }
        }

        public ICommand Add
        {
            get
            {
                return MakeCommand
                    .Do(async () =>
                    {
                        await _individual.AddScheduleAsync(_sessionPlace);
                    });
            }
        }

        public Visibility RemoveVisible
        {
            get { return _individual.IsScheduled(_sessionPlace) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public ICommand Remove
        {
            get
            {
                return MakeCommand
                    .Do(async () =>
                    {
                        await _individual.RemoveScheduleAsync(_sessionPlace);
                    });
            }
        }

        public Brush StatusBrush
        {
            get
            {
                string status;
                if (_individual.IsScheduled(_sessionPlace))
                    status = "ScheduledStatusBrush";
                else
                    status = "UnscheduledStatusBrush";
                return Application.Current.Resources[status] as Brush;
            }
        }
    }
}
