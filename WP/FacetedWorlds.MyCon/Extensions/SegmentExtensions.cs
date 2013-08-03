using System;
using FacetedWorlds.MyCon.Model;
using System.Linq;
using System.Collections.Generic;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon
{
    public static class SegmentExtensions
    {
        public static string GetString(this IEnumerable<DocumentSegment> segments)
        {
            if (segments == null)
                return null;
            return String.Join("", segments
                .Select(segment => segment.Text)
                .ToArray());
        }

        public static void SetString<T>(
            this TransientDisputable<T, IEnumerable<DocumentSegment>> property,
            string value,
            Action<TransientDisputable<T, IEnumerable<DocumentSegment>>> assign,
            ICommunity community)
             where T : CorrespondenceFact
        {
            List<DocumentSegment> segments = DocumentSegments(value, community);
            if (!SegmentsEqual(property.Ensure().Value, segments))
                assign(segments);
        }

        private static bool SegmentsEqual(IEnumerable<DocumentSegment> a, IEnumerable<DocumentSegment> b)
        {
            if (a == null)
                return b == null;
            if (b == null)
                return false;

            IEnumerator<DocumentSegment> aEnum = a.GetEnumerator();
            IEnumerator<DocumentSegment> bEnum = b.GetEnumerator();
            bool aNext = aEnum.MoveNext();
            bool bNext = bEnum.MoveNext();
            while (aNext && bNext)
            {
                if (aEnum.Current != bEnum.Current)
                    return false;
                aNext = aEnum.MoveNext();
                bNext = bEnum.MoveNext();
            }
            if ((aNext && !bNext) || bNext && !aNext)
                return false;
            return true;
        }

        private static List<DocumentSegment> DocumentSegments(string text, ICommunity community)
        {
            List<DocumentSegment> segments = new List<DocumentSegment>();
            while (!String.IsNullOrEmpty(text))
            {
                int segmentLength = Math.Min(512, text.Length);
                segments.Add(community.AddFact(new DocumentSegment(text.Substring(0, segmentLength))));
                text = text.Substring(segmentLength);
            }
            return segments;
        }
    }
}
