using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Attendee _attendee;

        public ScheduleViewModel(Attendee attendee)
        {
            _attendee = attendee;
        }

        public string ConferenceName
        {
            get { return _attendee.Conference.Name; }
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new ScheduleDayViewModel(day, _attendee);
            }
        }
    }
}
