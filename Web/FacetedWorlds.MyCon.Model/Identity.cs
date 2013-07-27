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
                if (IsToastNotificationEnabled.Any() && !value)
                {
                    Community.AddFact(new DisableToastNotification(IsToastNotificationEnabled));
                }
                else if (!IsToastNotificationEnabled.Any() && value)
                {
                    Community.AddFact(new EnableToastNotification(this));
                }
            }
        }

        public void JoinMessageBoard(string topic)
        {
            MessageBoard messageBoard = Community.AddFact(new MessageBoard(topic));
            Community.AddFact(new Share(this, messageBoard));
        }
    }
}
