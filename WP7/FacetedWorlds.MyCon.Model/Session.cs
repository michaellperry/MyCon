
using System.Linq;
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
            SessionPlace currentPlace = CurrentSessionPlaces.FirstOrDefault();
            if (currentPlace == null || currentPlace.Place != place)
                Community.AddFact(new SessionPlace(this, place, CurrentSessionPlaces));
        }
    }
}
