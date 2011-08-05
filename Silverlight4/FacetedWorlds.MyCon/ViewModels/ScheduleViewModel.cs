using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Conference _conference;

        public ScheduleViewModel(Conference conference)
        {
            _conference = conference;
        }

        public IEnumerable<ScheduleRowViewModel> Rows
        {
            get
            {
                return
                    from room in _conference.Rooms
                    orderby room.RoomNumber
                    select new ScheduleRowViewModel(room);
            }
        }

        public IEnumerable<ScheduleColumnHeaderViewModel> ColumnHeaders
        {
            get
            {
                return
                    from day in _conference.Days
                    from time in day.Times
                    orderby time.Start
                    select new ScheduleColumnHeaderViewModel(time);
            }
        }
    }
}
