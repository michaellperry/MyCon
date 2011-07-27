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
    }
}
