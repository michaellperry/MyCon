using System;
using System.Linq;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Identity
    {
        public bool ToastNotificationEnabled
        {
            get { return !IsToastNotificationDisabled.Any(); }
            set
            {
                if (IsToastNotificationDisabled.Any() && value)
                {
                    Community.AddFact(new EnableToastNotification(IsToastNotificationDisabled));
                }
                else if (!IsToastNotificationDisabled.Any() && !value)
                {
                    Community.AddFact(new DisableToastNotification(this));
                }
            }
        }

        public Attendee NewAttendee(string conferenceId)
        {
            Conference conference = Community.AddFact(new Conference(conferenceId));
            return Community.AddFact(new Attendee(this, conference));
        }
    }
}
