using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
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

        public string Contact
        {
            get { return _speaker.Contact; }
            set { _speaker.Contact = value; }
        }

        public string ImageUrl
        {
            get { return _speaker.ImageUrl; }
            set { _speaker.ImageUrl = value; }
        }

        public string Bio
        {
            get { return _speaker.Bio.Value.JoinSegments(); }
            set { _speaker.SetBio(value); }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            SpeakerDetailsViewModel that = obj as SpeakerDetailsViewModel;
            if (that == null)
                return false;
            return _speaker.Equals(that._speaker);
        }

        public override int GetHashCode()
        {
            return _speaker.GetHashCode();
        }
    }
}
