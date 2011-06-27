using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public static class SearchExtensions
    {
        public static bool AnyPartMatches(this string value, string searchTerm)
        {
            return value.ToLower().Split(' ').Any(part => part.StartsWith(searchTerm));
        }
    }
}
