using System;
using UpdateControls.Fields;

namespace $rootnamespace$.Models
{
    public class NavigationModel
    {
        private Independent<Identity> _currentUser = new Independent<Identity>();

        public Identity CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser.Value = value; }
        }
    }
}
