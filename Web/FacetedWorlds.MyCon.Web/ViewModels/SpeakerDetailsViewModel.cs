using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Web.Extensions;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SpeakerDetailsViewModel
    {
        private readonly Speaker _speaker;

        public SpeakerDetailsViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }

        public string Name
        {
            get { return _speaker.Name; }
        }

        public string ImageUrl
        {
            get { return _speaker.ImageUrl; }
        }

        public MvcHtmlString Bio
        {
            get
            {
                IEnumerable<DocumentSegment> segments = _speaker.Bio.Value;
                return segments.AsHtml();
            }
        }

        public IEnumerable<SessionViewModel> Sessions
        {
            get
            {
                return
                    from sessionPlace in _speaker.AvailableSessions
                    orderby sessionPlace.Place.PlaceTime.Start
                    select new SessionViewModel(sessionPlace);
            }
        }
    }
}