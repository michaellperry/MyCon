using FacetedWorlds.MyCon.Model;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Models
{
    public class SelectionModel
    {
        private Independent<SessionPlace> _selectedSessionPlace = new Independent<SessionPlace>();

        public SessionPlace SelectedSessionPlace
        {
            get { return _selectedSessionPlace.Value; }
            set { _selectedSessionPlace.Value = value; }
        }
    }
}
