using System;
using FacetedWorlds.MyCon.Model;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SpeakerViewModel
    {
        private readonly Speaker _speaker;

        public SpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }

        internal Speaker Speaker
        {
            get { return _speaker; }
        }

        public string Name
        {
            get { return _speaker.Name; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            SpeakerViewModel that = obj as SpeakerViewModel;
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
