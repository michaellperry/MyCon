using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleRowHeaderViewModel
    {
        private readonly Room _room;

        public ScheduleRowHeaderViewModel(Room room)
        {
            _room = room;
        }

        public string RoomNumber
        {
            get { return _room.RoomNumber; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduleRowHeaderViewModel that = obj as ScheduleRowHeaderViewModel;
            if (that == null)
                return false;
            return _room.Equals(that._room);
        }

        public override int GetHashCode()
        {
            return _room.GetHashCode();
        }
    }
}
