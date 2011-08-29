using System;
using System.Linq;
using UpdateControls.Correspondence;

namespace $rootnamespace$.Models
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
    }
}
