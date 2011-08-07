using System;
using FacetedWorlds.MyCon.Model;
using System.Linq;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.ViewModels
{
    public static class SegmentExtensions
    {
        public static string JoinSegments(this IEnumerable<DocumentSegment> segments)
        {
            if (segments == null)
                return null;
            return String.Join("", segments
                .Select(segment => segment.Text)
                .ToArray());
        }
    }
}
