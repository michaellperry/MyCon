using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Conference _conference;
        private readonly NavigationModel _navigationModel;

        public ScheduleViewModel(Conference conference, NavigationModel navigationModel)
        {
            _conference = conference;
            _navigationModel = navigationModel;
        }

        public IEnumerable<ScheduleRowViewModel> Rows
        {
            get
            {
                return
                    from room in _conference.Rooms
                    orderby room.RoomNumber.Value ?? room.Unique.ToString()
                    select new ScheduleRowViewModel(room);
            }
        }

        public IEnumerable<ScheduleRowHeaderViewModel> RowHeaders
        {
            get
            {
                return
                    from room in _conference.Rooms
                    orderby room.RoomNumber.Value ?? room.Unique.ToString()
                    select new ScheduleRowHeaderViewModel(room);
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

        public IEnumerable<ScheduledSessionViewModel> UnscheduledSessions
        {
            get
            {
                return
                    from session in _conference.UnscheduledSessions
                    orderby session.Name.Value
                    select new ScheduledSessionViewModel(session);
            }
        }

        public bool AnyUnscheduledSessions
        {
            get { return _conference.UnscheduledSessions.Any(); }
        }

        public string NewRoomNumber
        {
            get { return _navigationModel.NewRoomNumber; }
            set { _navigationModel.NewRoomNumber = value; }
        }

        public ICommand NewRoom
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_navigationModel.NewRoomNumber))
                    .Do(() =>
                    {
                        _conference.GetRoom(_navigationModel.NewRoomNumber);
                        _navigationModel.NewRoomNumber = null;
                    });
            }
        }

        public string NewTime
        {
            get { return _navigationModel.NewTime; }
            set { _navigationModel.NewTime = value; }
        }

        public ICommand AddTime
        {
            get
            {
                return MakeCommand
                    .When(() => IsValidDateTime(_navigationModel.NewTime))
                    .Do(() =>
                    {
                        DateTime startTime = DateTime.Parse(_navigationModel.NewTime).ToUniversalTime();
                        Time time = _conference.GetTime(startTime);
                        time.UnDelete();
                    });
            }
        }

        private static bool IsValidDateTime(string str)
        {
            DateTime value;
            return DateTime.TryParse(str, out value);
        }
    }
}
