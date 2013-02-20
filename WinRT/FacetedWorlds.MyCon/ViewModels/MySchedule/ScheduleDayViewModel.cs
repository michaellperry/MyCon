using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class ScheduleDayViewModel
    {
        private readonly Day _day;
        private readonly Func<Time, ScheduleTimeViewModel> _newScheduleTime;
        
        public ScheduleDayViewModel(Day day, Func<Time, ScheduleTimeViewModel> newScheduleTime)
        {
            _day = day;
            _newScheduleTime = newScheduleTime;
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _day.ConferenceDate.Date); }
        }

        public IEnumerable<ScheduleTimeViewModel> Slots
        {
            get
            {
                return
                    from time in _day.Times
                    orderby time.Start
                    select _newScheduleTime(time);
            }
        }
    }
}
