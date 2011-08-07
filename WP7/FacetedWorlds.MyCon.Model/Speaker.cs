using System;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Speaker
    {
        public void SetBio(string bio)
        {
            List<DocumentSegment> bioSegments = DocumentSegments(bio);
            if (!SegmentsEqual(Bio.Value, bioSegments))
                Bio = bioSegments;
        }

        public bool SegmentsEqual(IEnumerable<DocumentSegment> a, IEnumerable<DocumentSegment> b)
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

        public List<DocumentSegment> DocumentSegments(string text)
        {
            List<DocumentSegment> segments = new List<DocumentSegment>();
            while (!String.IsNullOrEmpty(text))
            {
                int segmentLength = Math.Min(512, text.Length);
                segments.Add(Community.AddFact(new DocumentSegment(text.Substring(0, segmentLength))));
                text = text.Substring(segmentLength);
            }
            return segments;
        }
    }
}
