using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Conference _conference;

        public ScheduleViewModel(Conference conference)
        {
            _conference = conference;
        }

        public IEnumerable<DayViewModel> Days
        {
            get
            {
                return
                    from day in _conference.Days
                    orderby day.ConferenceDate
                    select new DayViewModel(day);
            }
        }
    }
}