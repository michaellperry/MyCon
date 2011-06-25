
namespace FacetedWorlds.MyCon.Model
{
    public partial class Attendee
    {
        public Slot NewSlot(Time time)
        {
            return Community.AddFact(new Slot(this, time));
        }
    }
}
