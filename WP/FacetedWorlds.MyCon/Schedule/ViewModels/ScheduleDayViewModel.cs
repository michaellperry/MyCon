using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public class ScheduleDayViewModel
    {
        private readonly DateTime _date;
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;

        public ScheduleDayViewModel(DateTime date, Attendee attendee, ImageCache imageCache)
        {
            _date = date;
            _attendee = attendee;
            _imageCache = imageCache;
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _date); }
        }

        public IEnumerable<ScheduleSlotViewModel> Slots
        {
            get
            {
                return
                    from time in _attendee.Conference.Times
                    let startTime = time.StartTime.Value
                    where startTime.Date == _date
                    orderby startTime
                    select new ScheduleSlotViewModel(_attendee, time, _imageCache);
            }
        }
    }
}
