using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public ScheduleViewModel(SynchronizationService synchronizationService, Attendee attendee, ImageCache imageCache, SearchModel searchModel)
        {
            _synchronizationService = synchronizationService;
            _attendee = attendee;
            _imageCache = imageCache;
            _searchModel = searchModel;
        }

        public bool Loading
        {
            get { return _synchronizationService.Synchronizing; }
        }

        public string ConferenceName
        {
            get { return _attendee.Conference.Name.Value; }
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new ScheduleDayViewModel(day, _attendee, _imageCache);
            }
        }

        public void ClearSearch()
        {
            _searchModel.SearchTerm = string.Empty;
        }
    }
}
