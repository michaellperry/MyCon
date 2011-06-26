using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using UpdateControls;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleDayViewModel
    {
        private readonly Day _day;
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;

        public ScheduleDayViewModel(Day day, Attendee attendee, ImageCache imageCache)
        {
            _day = day;
            _attendee = attendee;
            _imageCache = imageCache;
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _day.ConferenceDate.Date); }
        }

        public IEnumerable<ScheduleSlotViewModel> Slots
        {
            get
            {
                return
                    from time in _day.Times
                    orderby time.Start
                    select new ScheduleSlotViewModel(_attendee.NewSlot(time), null, _imageCache);
            }
        }
    }
}
