using System;
using FacetedWorlds.MyCon.Model;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SlotViewModel
    {
        private readonly Slot _slot;
        private readonly ImageCache _imageCache;

        public SlotViewModel(Slot slot, ImageCache imageCache)
        {
            _slot = slot;
            _imageCache = imageCache;
        }

        public string Time
        {
            get
            {
                return String.Format(
                    "{0:dddd, MMMM d} at {1:h:mm tt}",
                    _slot.SlotTime.Day.ConferenceDate.ToLocalTime(),
                    _slot.SlotTime.Start.ToLocalTime());
            }
        }

        public string Instructions
        {
            get
            {
                List<Schedule> schedules = _slot.CurrentSchedules.ToList();
                return
                    schedules.Count == 0 ?
                        "Tap for session details" :
                    schedules.Count == 1 ?
                        String.Format("You are scheduled for {0}", schedules[0].SessionPlace.Session.Name.Value) :
                        "You are overbooked";
            }
        }

        public IEnumerable<TrackSessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _slot.SlotTime.AvailableSessions
                    select new TrackSessionViewModel(_slot, sessionPlace, _imageCache);
            }
        }
    }
}
