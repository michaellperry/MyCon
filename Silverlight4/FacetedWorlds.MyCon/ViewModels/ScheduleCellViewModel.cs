using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleCellViewModel
    {
        private readonly Place _place;

        public ScheduleCellViewModel(Place place)
        {
            _place = place;
        }

        public IEnumerable<ScheduledSessionViewModel> ScheduledSessions
        {
            get
            {
                return
                    from sessionPlace in _place.CurrentSessionPlaces
                    select new ScheduledSessionViewModel(sessionPlace);
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ScheduleCellViewModel that = obj as ScheduleCellViewModel;
            if (that == null)
                return false;
            return _place.Equals(that._place);
        }

        public override int GetHashCode()
        {
            return _place.GetHashCode();
        }
    }
}
