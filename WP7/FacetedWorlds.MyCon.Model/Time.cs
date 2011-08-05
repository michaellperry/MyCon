
namespace FacetedWorlds.MyCon.Model
{
    public partial class Time
    {
        public Place GetPlace(Room room)
        {
            return Community.AddFact(new Place(this, room));
        }
    }
}
