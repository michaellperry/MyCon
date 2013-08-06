using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public class SlotViewModel
    {
        private Time _time;
        private ImageCache _imageCache;

        public SlotViewModel(Time time, ImageCache imageCache)
        {
            _time = time;
            _imageCache = imageCache;
        }

        public string Time
        {
            get
            {
                return String.Format(
                    "{0:dddd, MMMM d} at {0:h:mm tt}",
                    _time.StartTime.Value.ToLocalTime());
            }
        }

        public string Instructions
        {
            get
            {
                return "Tap for session details";
            }
        }

        public IEnumerable<TrackSessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionSlot in _time.SessionSlots
                    select new TrackSessionViewModel(sessionSlot, _imageCache);
            }
        }
    }
}
