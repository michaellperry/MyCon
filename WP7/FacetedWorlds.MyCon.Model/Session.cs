
namespace FacetedWorlds.MyCon.Model
{
    public partial class Session
    {
        public bool Matches(string searchTerm)
        {
            string[] terms = searchTerm.Split(' ');
            return Name.Value.AnyPartMatches(terms)
                || Speaker.Name.AnyPartMatches(terms)
                || (Track != null && Track.Name.AnyPartMatches(terms));
        }
    }
}
