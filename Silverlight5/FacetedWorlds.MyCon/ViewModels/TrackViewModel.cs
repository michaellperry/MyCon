using System;
using FacetedWorlds.MyCon.Model;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TrackViewModel
    {
        private readonly Track _track;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        internal Track Track
        {
            get { return _track; }
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            TrackViewModel that = obj as TrackViewModel;
            if (that == null)
                return false;
            return _track.Equals(that._track);
        }

        public override int GetHashCode()
        {
            return _track.GetHashCode();
        }
    }
}
