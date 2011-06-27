using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Models
{
    public class SearchModel
    {
        private Independent<string> _searchTerm = new Independent<string>();

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm.Value = value; }
        }
    }
}
