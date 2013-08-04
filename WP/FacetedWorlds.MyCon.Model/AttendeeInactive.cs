using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Model
{
    public partial class AttendeeInactive
    {
        public void MakeActive()
        {
            Community.AddFact(new AttendeeActive(this));
        }
    }
}
