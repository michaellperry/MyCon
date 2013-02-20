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
        private readonly Func<SessionPlace, SessionHeaderViewModel> _newSessionHeaderViewModel;

        public TrackViewModel(Track track, Func<SessionPlace, SessionHeaderViewModel> newSessionHeaderViewModel)
        {
            _track = track;
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
                return
                    from sessionPlace in _track.CurrentSessionPlaces
                    where CanDisplay(sessionPlace)
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

        public static bool CanDisplay(SessionPlace sessionPlace)
        {
            return sessionPlace.Place != null
                && sessionPlace.Place.PlaceTime != null;
        }
    }
}
