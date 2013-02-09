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
        private readonly Individual _individual;
        
        public ScheduleTimeViewModel(Time time, Individual individual)
        {
            _time = time;
            _individual = individual;
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
                    .Select(schedule => new ScheduleSlotViewModel(_time, _individual, schedule));
            }
        }
    }
}
