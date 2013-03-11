using System;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public static class Container
    {
        public static MyScheduleViewModel CreateViewModel(
            Conference conference,
            Individual individual,
            SelectionModel selection)
        {
            Func<Time, Schedule, ScheduleSlotViewModel> newScheduleSlot = (time, schedule) =>
                new ScheduleSlotViewModel(time, individual, schedule, selection);

            Func<Time, ScheduleTimeViewModel> newScheduleTime = time =>
                new ScheduleTimeViewModel(time, individual, selection, newScheduleSlot);

            Func<Day, ScheduleDayViewModel> newScheduleDay = day =>
                new ScheduleDayViewModel(day, newScheduleTime);

            return new MyScheduleViewModel(conference, selection, newScheduleDay);
        }
    }
}
