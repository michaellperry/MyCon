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
                        _conference.NewRoom(_navigationModel.NewRoomNumber);
                        _navigationModel.NewRoomNumber = null;
                    });
            }
        }
    }
}
