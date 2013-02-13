using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class TrackViewModel
    {
        private readonly Track _track;
        private readonly SelectionModel _selectionModel;
        
        public TrackViewModel(Track track, SelectionModel selectionModel)
        {
            _track = track;
            _selectionModel = selectionModel;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SessionHeaderViewModel> Items
        {
            get
            {
                var sessionPlaces = _track.CurrentSessionPlaces.ToList();
                return
                    from sessionPlace in sessionPlaces
                    where CanDisplay(sessionPlace)
                    orderby sessionPlace.Place.PlaceTime.Start
                    select new SessionHeaderViewModel(sessionPlace, _selectionModel);
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
