using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public ScheduleViewModel(Attendee attendee, ImageCache imageCache, SearchModel searchModel)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _searchModel = searchModel;
        }

        public string ConferenceName
        {
            get { return _attendee.Conference.Name; }
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
