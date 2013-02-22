using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class ScheduleTimeViewModel
    {
        private readonly Time _time;
        private readonly Individual _individual;
        private readonly Func<Time, Schedule, ScheduleSlotViewModel> _newScheduleSlot;
        
        public ScheduleTimeViewModel(Time time, Individual individual, Func<Time, Schedule, ScheduleSlotViewModel> newScheduleSlot)
        {
            _time = time;
            _individual = individual;
            _newScheduleSlot = newScheduleSlot;
        }

        public string Time
        {
            get { return String.Format("{0:h:mm}", _time.Start.ToLocalTime()); }
        }

        public IEnumerable<ScheduleSlotViewModel> Schedules
        {
            get
            {
                var schedules = _individual.Attendees
                    .SelectMany(a => a.CurrentSchedules)
                    .Where(schedule =>
                        schedule.Slot != null &&
                        schedule.Slot.SlotTime == _time)
                    .DefaultIfEmpty();
                return schedules
                    .Select(schedule => _newScheduleSlot(_time, schedule));
            }
        }
    }
}
