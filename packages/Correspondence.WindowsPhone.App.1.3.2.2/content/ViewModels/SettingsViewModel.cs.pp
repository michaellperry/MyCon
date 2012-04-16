using System;
using System.Linq;

namespace $rootnamespace$.ViewModels
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
