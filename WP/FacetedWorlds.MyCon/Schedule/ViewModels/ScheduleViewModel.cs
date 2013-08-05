using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Schedule.Models;
using Microsoft.Phone.Shell;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
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
            get { return _synchronizationService.Community.Synchronizing; }
        }

        public string ConferenceName
        {
            get
            {
                return _attendee.Conference.ConferenceHeaders
                    .Select(ch => ch.Name.Value)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<ScheduleDayViewModel> Days
        {
            get
            {
                var dates =
                    from time in _attendee.Conference.Times
                    select time.StartTime.Value.Date;
                return
                    from date in dates.Distinct()
                    orderby date
                    select new ScheduleDayViewModel(date, _attendee, _imageCache);
            }
        }

        public void ClearSearch()
        {
            _searchModel.SearchTerm = string.Empty;
        }

        public ShellTileData GetTileData()
        {
            var conferenceHeader = _attendee.Conference.ConferenceHeaders.Ensure()
                .FirstOrDefault();
            if (conferenceHeader == null)
                return null;

            Uri imageUri = new Uri(conferenceHeader.ImageUrl.Ensure(), UriKind.Absolute);
            return new StandardTileData()
            {
               Title = conferenceHeader.Name.Ensure(),
               BackTitle = conferenceHeader.Name.Ensure(),
               BackContent = "Your next session...",
               Count = 0,
               BackgroundImage = imageUri,
               BackBackgroundImage = null
            };
        }

        public void LeaveConference()
        {
            _attendee.MakeInactive();
        }
    }
}
