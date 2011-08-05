using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleColumnHeaderViewModel
    {
        private readonly Time _time;

        public ScheduleColumnHeaderViewModel(Time time)
        {
            _time = time;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduleColumnHeaderViewModel that = obj as ScheduleColumnHeaderViewModel;
            if (that == null)
                return false;
            return _time.Equals(that._time);
        }

        public override int GetHashCode()
        {
            return _time.GetHashCode();
        }
    }
}
