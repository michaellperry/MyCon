using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public class ScheduleSlotViewModel
    {
        private readonly Attendee _attendee;
        private readonly Time _time;
        private readonly ImageCache _imageCache;

        public ScheduleSlotViewModel(Attendee attendee, Time time, ImageCache imageCache)
        {
            _attendee = attendee;
            _time = time;
            _imageCache = imageCache;
        }

        public Guid ConferenceId
        {
            get { return _attendee.Conference.Unique; }
        }

        public Guid TimeId
        {
            get { return _time.Unique; }
        }

        public string Time
        {
            get { return String.Format("{0:h:mm}", _time.StartTime.Value.ToLocalTime()); }
        }

        public CachedImage ImageUrl
        {
            get
            {
                return new CachedImage { ImageUrl = "/Assets/Images/unknown.small.png" };
            }
        }

        public string Title
        {
            get
            {
                return "Breakout Session";
            }
        }

        public string Speaker
        {
            get
            {
                return "Tap for choices";
            }
        }

        public string Room
        {
            get
            {
                return String.Empty;
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

        public string TargetUri
        {
            get
            {
                return String.Format("/Times/Views/TimePage.xaml?StartTime={0}", _time.StartTime.Value);
            }
        }
    }
}
