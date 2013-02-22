using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Models
{
    public class SelectionModel
    {
        private Independent<SessionPlace> _selectedSessionPlace = new Independent<SessionPlace>();
        private Independent<Time> _selectedTime = new Independent<Time>();

        public SessionPlace SelectedSessionPlace
        {
            get { return _selectedSessionPlace.Value; }
            set { _selectedSessionPlace.Value = value; }
        }

        public Time SelectedTime
        {
            get { return _selectedTime; }
            set { _selectedTime.Value = value; }
        }
    }
}
