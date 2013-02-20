using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using System;

namespace FacetedWorlds.MyCon.ViewModels.MySchedule
{
    public class MyScheduleViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly Conference _conference;
        private readonly Individual _individual;
        private readonly SearchModel _searchModel;
        
        public MyScheduleViewModel(
            SynchronizationService synchronizationService, 
            Conference conference, 
            Individual individual, 
            SearchModel searchModel)
        {
            _synchronizationService = synchronizationService;
            _conference = conference;
            _individual = individual;
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
                return _conference.Name.Value;
            }
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                return
                    from day in _conference.Days
                    orderby day.ConferenceDate
                    select new ScheduleDayViewModel(day, _individual);
            }
        }

        public void ClearSearch()
        {
            _searchModel.SearchTerm = string.Empty;
        }
    }
}
