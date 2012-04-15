using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class TrackViewModel
    {
        private readonly Track _track;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SpeakerViewModel> Speakers
        {
            get
            {
                var speakers = _track.CurrentSessionPlaces
                    .Select(sessionPlace => sessionPlace.Session.Speaker)
                    .Distinct();
                return
                    from speaker in speakers
                    orderby speaker.Name
                    select new SpeakerViewModel(speaker);
            }
        }
    }
}
