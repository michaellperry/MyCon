using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public static class Container
    {
        public static MyScheduleViewModel CreateViewModel(
            Conference conference,
            Individual individual,
            SelectionModel selection)
        {
            var frame = Window.Current.Content as Frame;
                        
            Action<Time> showTime = time =>
            {
                selection.SelectedSessionPlace = time.AvailableSessions.FirstOrDefault();
                frame.Navigate(typeof(SessionView));
            };

            Func<Time, Schedule, ScheduleSlotViewModel> newScheduleSlot = (time, schedule) =>
                new ScheduleSlotViewModel(time, individual, schedule, showTime);

            Func<Time, ScheduleTimeViewModel> newScheduleTime = time =>
                new ScheduleTimeViewModel(time, individual, newScheduleSlot);

            Func<Day, ScheduleDayViewModel> newScheduleDay = day =>
                new ScheduleDayViewModel(day, newScheduleTime);

            return new MyScheduleViewModel(conference, newScheduleDay);
        }
    }
}
