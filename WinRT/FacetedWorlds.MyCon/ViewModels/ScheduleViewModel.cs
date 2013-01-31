using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly Attendee _attendee;
        private readonly SearchModel _searchModel;

        public ScheduleViewModel(SynchronizationService synchronizationService, Attendee attendee, SearchModel searchModel)
        {
            _synchronizationService = synchronizationService;
            _attendee = attendee;
            _searchModel = searchModel;
        }

        public bool Loading
        {
            get
            {
                return
                    _synchronizationService.Community != null &&
                    _synchronizationService.Community.Synchronizing;
            }
        }

        public string ConferenceName
        {
            get
            {
                if (_attendee.Conference == null)
                    return null;

                return _attendee.Conference.Name.Value;
            }
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new ScheduleDayViewModel(day, _attendee);
            }
        }

        public void ClearSearch()
        {
            _searchModel.SearchTerm = string.Empty;
        }
    }
}
