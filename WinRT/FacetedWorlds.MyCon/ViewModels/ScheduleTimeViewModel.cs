using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleTimeViewModel
    {
        private readonly Time _time;
        private readonly Attendee _attendee;

        public ScheduleTimeViewModel(Time time, Attendee attendee)
        {
            _time = time;
            _attendee = attendee;
        }

        public IEnumerable<ScheduleSlotViewModel> Schedules
        {
            get
            {
                var schedules = _attendee.CurrentSchedules
                    .Where(schedule =>
                        schedule.Slot != null &&
                        schedule.Slot.SlotTime == _time)
                    .DefaultIfEmpty();
                return schedules
                    .Select(schedule => new ScheduleSlotViewModel(_time, _attendee, schedule));
            }
        }
    }
}
