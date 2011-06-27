using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public static class SearchExtensions
    {
        public static bool AnyPartMatches(this string value, IEnumerable<string> terms)
        {
            string[] values = value.ToLower().Split(' ');
            return terms.All(term => values.Any(part => part.StartsWith(term)));
        }
    }
}
