using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class TracksViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly SelectionModel _selectionModel;
        
        public TracksViewModel(SynchronizationService synchronizationService, SelectionModel selectionModel)
        {
            _synchronizationService = synchronizationService;
            _selectionModel = selectionModel;
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
                    select new TrackViewModel(track, _selectionModel);
            }
        }
    }
}
