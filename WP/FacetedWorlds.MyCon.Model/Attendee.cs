using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Attendee
    {
        public void MakeInactive()
        {
            if (!Inactives.Any())
                Community.AddFact(new AttendeeInactive(this));
        }
    }
}
