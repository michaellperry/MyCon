using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels.AllSessions
{
    public class AllSessionsViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly Func<Track, TrackViewModel> _newTrackViewModel;

        public AllSessionsViewModel(SynchronizationService synchronizationService, Func<Track, TrackViewModel> newTrackViewModel)
        {
            _synchronizationService = synchronizationService;
            _newTrackViewModel = newTrackViewModel;
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
                    select _newTrackViewModel(track);
            }
        }
    }
}
