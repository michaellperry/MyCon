
using System.Threading.Tasks;
namespace FacetedWorlds.MyCon.Model
{
    public partial class Attendee
    {
        public Task<Slot> NewSlot(Time time)
        {
            return Community.AddFactAsync(new Slot(this, time));
        }
    }
}
