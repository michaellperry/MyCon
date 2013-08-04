using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Individual
    {
        public Profile EnsureProfile()
        {
            var profile = Profiles.Ensure().FirstOrDefault();
            if (profile == null)
            {
                profile = Community.AddFact(new Profile(Guid.NewGuid().ToString()));
                Community.AddFact(new IndividualProfile(this, profile));
            }
            return profile;
        }
    }
}
