using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SettingsViewModel
    {
        private Identity _identity;

        public SettingsViewModel(Identity identity)
        {
            _identity = identity;
        }

        public bool EnableToastNotification
        {
            get { return _identity.ToastNotificationEnabled; }
            set { _identity.ToastNotificationEnabled = value; }
        }
    }
}
