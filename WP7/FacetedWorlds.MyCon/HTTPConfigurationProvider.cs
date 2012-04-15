using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon
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
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
                return new HTTPConfiguration(address, "FacetedWorlds.MyCon", apiKey);
            }
        }

        public bool IsToastEnabled
        {
            get { return Identity == null ? false : Identity.ToastNotificationEnabled; }
        }
    }
}
