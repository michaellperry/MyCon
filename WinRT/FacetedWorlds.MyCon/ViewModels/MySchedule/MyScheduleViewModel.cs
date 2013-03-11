using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using Windows.UI.Xaml;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class MyScheduleViewModel
    {
        private readonly Conference _conference;
        private readonly SelectionModel _selection;
        private readonly Func<Day, ScheduleDayViewModel> _newScheduleDay;
        
        public MyScheduleViewModel(
            Conference conference,
            SelectionModel selection,
            Func<Day, ScheduleDayViewModel> newScheduleDay)
        {
            _conference = conference;
            _selection = selection;
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

        public bool HasSelection
        {
            get { return _selection.SelectedTime != null; }
        }

        public Visibility VisibleWhenNoSelection
        {
            get
            {
                return _selection.SelectedTime == null
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility VisibleWhenSelection
        {
            get
            {
                return _selection.SelectedTime == null
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }
    }
}
