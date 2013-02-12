using System;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class TracksViewModel
    {
        private readonly SynchronizationService _synchronizationService;

        public TracksViewModel(SynchronizationService synchronizationService)
        {
            _synchronizationService = synchronizationService;
        }

        public bool Synchronizing
        {
            get { return _synchronizationService.Community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                Exception exception = _synchronizationService.Community.LastException;
                return exception == null ? null : exception.Message;
            }
        }

        public string Conference
        {
            get
            {
                if (_synchronizationService.Conference == null)
                    return null;

                return _synchronizationService.Conference.Name;
            }
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                if (_synchronizationService.Conference == null)
                    return null;

                return
                    from track in _synchronizationService.Conference.Tracks
                    where track.CurrentSessionPlaces.Any(sp => TrackViewModel.CanDisplay(sp))
                    orderby track.Name
                    select new TrackViewModel(track);
            }
        }
    }
}
