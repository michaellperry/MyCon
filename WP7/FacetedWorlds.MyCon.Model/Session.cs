
using System.Linq;
using System.Collections.Generic;
using System;
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

        public void SetSessionPlace(Time time, string roomNumber)
        {
            Room room = Community.AddFact(new Room(Conference, roomNumber));
            Place place = Community.AddFact(new Place(time, room));
            SetPlace(place);
        }

        public void SetPlace(Place place)
        {
            List<SessionPlace> currentSessionPlaces = CurrentSessionPlaces.ToList();
            if (currentSessionPlaces.Count != 1 || currentSessionPlaces[0].Place != place)
                Community.AddFact(new SessionPlace(this, place, CurrentSessionPlaces));
        }

        public void Delete()
        {
            Community.AddFact(new SessionDelete(this));
        }

        public void SetDescription(string description)
        {
            List<DocumentSegment> descriptionSegments = DocumentSegments(description);
            if (!SegmentsEqual(Description.Value, descriptionSegments))
                Description = descriptionSegments;
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
