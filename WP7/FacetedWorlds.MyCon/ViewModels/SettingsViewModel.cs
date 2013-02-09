using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SettingsViewModel
    {
        private Individual _individual;
        private Attendee _attendee;

        public SettingsViewModel(Individual individual, Attendee attendee)
        {
            _individual = individual;
            _attendee = attendee;
        }

        public string ConferenceName
        {
            get { return _attendee.Conference.Name; }
        }

        public bool EnableToastNotification
        {
            get { return _individual.ToastNotificationEnabled; }
            set { _individual.ToastNotificationEnabled = value; }
        }
    }
}
