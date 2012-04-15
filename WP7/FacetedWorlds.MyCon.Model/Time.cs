
namespace FacetedWorlds.MyCon.Model
{
    public partial class Time
    {
        public Place GetPlace(Room room)
        {
            return Community.AddFact(new Place(this, room));
        }

        public void Delete()
        {
            Community.AddFact(new TimeDelete(this));
        }

        public void UnDelete()
        {
            foreach (TimeDelete del in Deletes)
            {
                Community.AddFact(new TimeUndelete(del));
            }
        }
    }
}
