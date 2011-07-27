using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SettingsViewModel
    {
        private Attendee _attendee;

        public SettingsViewModel(Attendee attendee)
        {
            _attendee = attendee;
        }

        public string ConferenceName
        {
            get { return _attendee.Conference.Name; }
        }

        public bool EnableToastNotification
        {
            get { return _attendee.Identity.ToastNotificationEnabled; }
            set { _attendee.Identity.ToastNotificationEnabled = value; }
        }
    }
}
