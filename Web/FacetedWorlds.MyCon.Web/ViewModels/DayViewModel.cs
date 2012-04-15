using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class DayViewModel
    {
        private readonly Day _day;

        public DayViewModel(Day day)
        {
            _day = day;
        }

        public string Text
        {
            get { return string.Format("{0:dddd MMMM d}", _day.ConferenceDate); }
        }

        public IEnumerable<TimeViewModel> Times
        {
            get
            {
                return
                    from time in _day.Times
                    orderby time.Start
                    select new TimeViewModel(time);
            }
        }
    }
}
