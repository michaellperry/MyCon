using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using System;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private readonly Identity _identity;
        private readonly SynchronizationService _synhronizationService;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public MainViewModel(Identity identity, SynchronizationService synhronizationService, ImageCache imageCache, SearchModel searchModel)
        {
            _identity = identity;
            _synhronizationService = synhronizationService;
            _imageCache = imageCache;
            _searchModel = searchModel;
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }

        public ScheduleViewModel Schedule
        {
            get { return new ScheduleViewModel(Attendee, _imageCache, _searchModel); }
        }

        public TracksViewModel Tracks
        {
            get { return new TracksViewModel(Attendee, _imageCache, _searchModel); }
        }

        public SearchViewModel Search
        {
            get { return new SearchViewModel(Attendee, _imageCache, _searchModel); }
        }

        private Attendee Attendee
        {
            get { return _identity.NewAttendee("Conference ID"); }
        }
    }
}
