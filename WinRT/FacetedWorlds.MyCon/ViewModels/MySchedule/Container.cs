using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public static class Container
    {
        public static MyScheduleViewModel CreateViewModel(
            Conference conference,
            Individual individual)
        {
            Func<Time, Schedule, ScheduleSlotViewModel> newScheduleSlot = (time, schedule) =>
                new ScheduleSlotViewModel(time, individual, schedule);

            Func<Time, ScheduleTimeViewModel> newScheduleTime = time =>
                new ScheduleTimeViewModel(time, individual, newScheduleSlot);

            Func<Day, ScheduleDayViewModel> newScheduleDay = day =>
                new ScheduleDayViewModel(day, newScheduleTime);

            return new MyScheduleViewModel(conference, newScheduleDay);
        }
    }
}
