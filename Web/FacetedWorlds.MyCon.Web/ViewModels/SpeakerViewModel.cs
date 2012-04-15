using FacetedWorlds.MyCon.Model;
using System.Linq;
using System.Collections.Generic;

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
