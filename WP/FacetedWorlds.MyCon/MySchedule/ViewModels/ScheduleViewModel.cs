using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.MySchedule.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Attendee _attendee;

        public ScheduleViewModel(Attendee attendee)
        {
            _attendee = attendee;
        }

        public void LeaveConference()
        {
            _attendee.MakeInactive();
        }
    }
}
