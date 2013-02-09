using System;
using System.Linq;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Individual
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

        public Attendee GetAttendee(string conferenceId)
        {
            Attendee attendee = this.Attendees.Ensure()
                .Where(a => a.Conference.Id == conferenceId)
                .FirstOrDefault();
            if (attendee == null)
            {
                Conference conference = Community.AddFact(new Conference(conferenceId));
                string identifier = Guid.NewGuid().ToString();
                attendee = Community.AddFact(new Attendee(conference, identifier));
                Community.AddFact(new IndividualAttendee(this, attendee));
            }

            return attendee;
        }
    }
}
