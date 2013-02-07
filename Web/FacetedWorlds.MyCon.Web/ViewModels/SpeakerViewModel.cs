using FacetedWorlds.MyCon.Model;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.Extensions;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SpeakerViewModel
    {
        private readonly Speaker _speaker;

        public SpeakerViewModel(Speaker speaker)
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
    }
}
