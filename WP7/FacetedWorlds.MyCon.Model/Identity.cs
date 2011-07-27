using System;
using System.Linq;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Identity
    {
        public bool ToastNotificationEnabled
        {
            get { return IsToastNotificationEnabled.Any(); }
            set
            {
                if (ToastNotificationEnabled && !value)
                {
                    Community.AddFact(new DisableToastNotification(IsToastNotificationEnabled));
                }
                else if (!ToastNotificationEnabled && value)
                {
                    Community.AddFact(new EnableToastNotification(this));
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
