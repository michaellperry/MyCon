using System;
using UpdateControls.Fields;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Models
{
    public class NavigationModel
    {
        private Independent<Conference> _selectedConference = new Independent<Conference>();

        public Conference SelectedConference
        {
            get { return _selectedConference; }
            set { _selectedConference.Value = value; }
        }
    }
}
