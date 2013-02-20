using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class MyScheduleViewModel
    {
        private readonly Conference _conference;
        private readonly Func<Day, ScheduleDayViewModel> _newScheduleDay;
        
        public MyScheduleViewModel(
            Conference conference,
            Func<Day, ScheduleDayViewModel> newScheduleDay)
        {
            _conference = conference;
            _newScheduleDay = newScheduleDay;
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                return
                    from day in _conference.Days
                    orderby day.ConferenceDate
                    select _newScheduleDay(day);
            }
        }
    }
}
