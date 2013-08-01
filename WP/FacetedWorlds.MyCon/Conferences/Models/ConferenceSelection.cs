using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Conferences.Models
{
    public class ConferenceSelection
    {
        private Independent<ConferenceHeader> _selectedConference =
            new Independent<ConferenceHeader>();

        public ConferenceHeader SelectedConference
        {
            get { return _selectedConference; }
            set { _selectedConference.Value = value; }
        }
    }
}
