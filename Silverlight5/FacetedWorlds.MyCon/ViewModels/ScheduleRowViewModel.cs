using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleRowViewModel
    {
        private readonly Room _room;

        public ScheduleRowViewModel(Room room)
        {
            _room = room;
        }

        public IEnumerable<ScheduleCellViewModel> Cells
        {
            get
            {
                return
                    from day in _room.Conference.Days
                    from time in day.Times
                    orderby time.Start
                    select new ScheduleCellViewModel(time.GetPlace(_room));
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduleRowViewModel that = obj as ScheduleRowViewModel;
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
