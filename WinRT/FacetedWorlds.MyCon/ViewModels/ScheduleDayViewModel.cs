using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleDayViewModel
    {
        private readonly Day _day;
        private readonly Individual _individual;
        
        public ScheduleDayViewModel(Day day, Individual individual)
        {
            _day = day;
            _individual = individual;
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
                    select new ScheduleTimeViewModel(time, _individual);
            }
        }
    }
}
