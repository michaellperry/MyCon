using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

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

        public string Bio
        {
            get
            {
                IEnumerable<DocumentSegment> segments = _speaker.Bio.Value;
                if (segments == null)
                    return string.Empty;

                return string.Join("", segments.Select(segment => segment.Text).ToArray());
            }
        }
    }
}