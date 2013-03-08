using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.AllSessions
{
    public class AllSessionsViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly SearchModel _search;
        private readonly Func<Track, TrackViewModel> _newTrackViewModel;

        public AllSessionsViewModel(
            SynchronizationService synchronizationService,
            SearchModel search,
            Func<Track, TrackViewModel> newTrackViewModel)
        {
            _synchronizationService = synchronizationService;
            _search = search;
            _newTrackViewModel = newTrackViewModel;
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                if (_synchronizationService.Conference == null)
                    return null;

                string searchTerm = _search.SearchTerm;
                if (searchTerm != null)
                    searchTerm = searchTerm.ToLower();

                return
                    from track in _synchronizationService.Conference.Tracks
                    where track.CurrentSessionPlaces.Any(sp => TrackViewModel.CanDisplay(sp, searchTerm))
                    orderby track.Name
                    select _newTrackViewModel(track);
            }
        }
    }
}
