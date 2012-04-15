using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class TracksViewModel
    {
        private readonly Conference _conference;

        public TracksViewModel(Conference conference)
        {
            _conference = conference;
        }

        public string Title
        {
            get { return String.Format("{0} Tracks", _conference.Name); }
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _conference.Tracks
                    orderby track.Name
                    select new TrackViewModel(track);
            }
        }
    }
}