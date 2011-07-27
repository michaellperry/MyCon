using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.POXClient;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon
{
    public class POXConfigurationProvider : IPOXConfigurationProvider
    {
        private Independent<Identity> _identity = new Independent<Identity>();

        public Identity Identity
        {
            get { return _identity; }
            set { _identity.Value = value; }
        }

        public POXConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/pox";
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
                return new POXConfiguration(address, "FacetedWorlds.MyCon", apiKey);
            }
        }

        public bool IsToastEnabled
        {
            get { return Identity == null ? false : Identity.ToastNotificationEnabled; }
        }
    }
}
