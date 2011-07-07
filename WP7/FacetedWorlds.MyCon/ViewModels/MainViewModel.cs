using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private readonly Attendee _attendee;
        private readonly SynchronizationService _synhronizationService;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;
        private readonly Clock _clock;

        public MainViewModel(Attendee attendee, SynchronizationService synhronizationService, ImageCache imageCache, SearchModel searchModel, Clock clock)
        {
            _attendee = attendee;
            _synhronizationService = synhronizationService;
            _imageCache = imageCache;
            _searchModel = searchModel;
            _clock = clock;
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }

        public ScheduleViewModel Schedule
        {
            get { return new ScheduleViewModel(_synhronizationService, _attendee, _imageCache, _searchModel); }
        }

        public TracksViewModel Tracks
        {
            get { return new TracksViewModel(_attendee, _imageCache, _searchModel); }
        }

        public SearchViewModel Search
        {
            get { return new SearchViewModel(_attendee, _imageCache, _searchModel); }
        }

        public MapViewModel Map
        {
            get { return new MapViewModel(_attendee, _imageCache, _clock); }
        }

        public NoticesViewModel Notices
        {
            get { return new NoticesViewModel(_attendee, _imageCache, _clock); }
        }
    }
}
