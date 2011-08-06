using System;
using UpdateControls.Fields;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Models
{
    public class NavigationModel
    {
        private Independent<Conference> _selectedConference = new Independent<Conference>();
        private Independent<string> _newRoomNumber = new Independent<string>();

        public Conference SelectedConference
        {
            get { return _selectedConference; }
            set { _selectedConference.Value = value; }
        }

        public string NewRoomNumber
        {
            get { return _newRoomNumber; }
            set { _newRoomNumber.Value = value; }
        }
    }
}
