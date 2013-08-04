using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Profile
    {
        public Attendee Attending(Conference conference)
        {
            return Community.AddFact(new Attendee(conference, this));
        }
    }
}
