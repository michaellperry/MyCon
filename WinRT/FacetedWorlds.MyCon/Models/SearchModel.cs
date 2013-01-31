using UpdateControls.Fields;
using System;

namespace FacetedWorlds.MyCon.Models
{
    public class SearchModel
    {
        private Independent<string> _searchTerm = new Independent<string>();
        private Independent<string> _selectedTrack = new Independent<string>();

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm.Value = value; }
        }

        public string SelectedTrack
        {
            get { return _selectedTrack; }
            set { _selectedTrack.Value = value; }
        }
    }
}
