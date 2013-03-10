using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class ScheduleTimeViewModel
    {
        private readonly Time _time;
        private readonly Individual _individual;
        private readonly SelectionModel _selection;
        private readonly Func<Time, Schedule, ScheduleSlotViewModel> _newScheduleSlot;

        public ScheduleTimeViewModel(
            Time time,
            Individual individual,
            SelectionModel selection,
            Func<Time, Schedule, ScheduleSlotViewModel> newScheduleSlot)
        {
            _time = time;
            _individual = individual;
            _selection = selection;
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

        public bool IsSelected
        {
            get { return _selection.SelectedTime == _time; }
        }
    }
}
