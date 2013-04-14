using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.AllSessions
{
    public class TrackViewModel
    {
        private readonly Track _track;
        private readonly SearchModel _search;
        private readonly Func<SessionPlace, SessionHeaderViewModel> _newSessionHeaderViewModel;

        public TrackViewModel(
            Track track,
            SearchModel search,
            Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel)
        {
            _track = track;
            _search = search;
            _newSessionHeaderViewModel = newSessionHeaderViewModel;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SessionHeaderViewModel> Items
        {
            get
            {
                string searchTerm = _search.SearchTerm;
                if (searchTerm != null)
                    searchTerm = searchTerm.ToLower();

                return
                    from sessionPlace in _track.CurrentSessionPlaces
                    where CanDisplay(sessionPlace, searchTerm)
                    orderby sessionPlace.Place.PlaceTime.Start
                    select _newSessionHeaderViewModel(sessionPlace);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            TrackViewModel that = obj as TrackViewModel;
            if (that == null)
                return false;
            return Object.Equals(this._track, that._track);
        }

        public override int GetHashCode()
        {
            return _track.GetHashCode();
        }

        public static bool CanDisplay(SessionPlace sessionPlace, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return true;

            bool hasSessionInfo =
                sessionPlace.Session.Name.Value != null &&
                sessionPlace.Session.Speaker.Name != null &&
                sessionPlace.Session.Track.Name != null &&
                sessionPlace.Session.Description.Value.Any();
            if (!hasSessionInfo)
                return false;

            return
                sessionPlace.Session.Speaker.Name.ToLower().Contains(searchTerm) ||
                sessionPlace.Session.Name.Value.ToLower().Contains(searchTerm) ||
                sessionPlace.Session.Track.Name.ToLower().Contains(searchTerm) ||
                sessionPlace.Session.Description.Value.JoinSegments().ToLower().Contains(searchTerm);
        }
    }
}
