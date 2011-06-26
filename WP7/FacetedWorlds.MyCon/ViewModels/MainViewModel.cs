using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private Identity _identity;
        private SynchronizationService _synhronizationService;
        private ImageCache _imageCache;

        public MainViewModel(Identity identity, SynchronizationService synhronizationService)
        {
            _identity = identity;
            _synhronizationService = synhronizationService;
            _imageCache = new ImageCache();
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }

        public ScheduleViewModel Schedule
        {
            get { return new ScheduleViewModel(Attendee, _imageCache); }
        }

        public TracksViewModel Tracks
        {
            get { return new TracksViewModel(Attendee, _imageCache); }
        }

        private Attendee Attendee
        {
            get { return _identity.NewAttendee("Conference ID"); }
        }
    }
}
