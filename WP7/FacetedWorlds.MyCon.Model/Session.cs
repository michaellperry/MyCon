
namespace FacetedWorlds.MyCon.Model
{
    public partial class Session
    {
        public bool Matches(string searchTerm)
        {
            return Name.Value.AnyPartMatches(searchTerm)
                || Speaker.Name.AnyPartMatches(searchTerm)
                || (Track != null && Track.Name.AnyPartMatches(searchTerm));
        }
    }
}
