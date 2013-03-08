using System;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.AllSessions
{
    public static class Container
    {
        public static AllSessionsViewModel CreateViewModel(
            SelectionModel selectionModel,
            SearchModel search,
            SynchronizationService synchronizationService)
        {
            Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel = s =>
                new SessionHeaderViewModel(s, selectionModel);

            Func<Track, TrackViewModel> newTrackViewModel = t =>
                new TrackViewModel(t, search, newSessionHeaderViewModel);

            return new AllSessionsViewModel(synchronizationService, search, newTrackViewModel);
        }
    }
}
