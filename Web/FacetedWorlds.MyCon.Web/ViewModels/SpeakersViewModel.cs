using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SpeakersViewModel
    {
        private readonly Conference _conference;

        public SpeakersViewModel(Conference conference)
        {
            _conference = conference;
        }

        public string Title
        {
            get { return _conference.Name + " Speakers"; }
        }

        public IEnumerable<SpeakerViewModel> Speakers
        {
            get
            {
                return
                    from s in _conference.Speakers
                    where !string.IsNullOrEmpty(s.Name)
                    orderby s.Name
                    select new SpeakerViewModel(s);
            }
        }
    }
}