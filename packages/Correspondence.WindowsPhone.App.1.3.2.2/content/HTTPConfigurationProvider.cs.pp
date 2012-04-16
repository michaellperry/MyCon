using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;

namespace $rootnamespace$
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        private Independent<Identity> _identity = new Independent<Identity>();

        public Identity Identity
        {
            get { return _identity; }
            set { _identity.Value = value; }
        }

        public HTTPConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/bin";
                string apiKey = "<<Your API key>>";
                return new HTTPConfiguration(address, "$rootnamespace$", apiKey);
            }
        }

        public bool IsToastEnabled
        {
            get { return Identity == null ? false : Identity.ToastNotificationEnabled; }
        }
    }
}
