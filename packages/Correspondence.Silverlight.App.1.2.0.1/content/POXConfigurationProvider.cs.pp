using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.POXClient;
using $rootnamespace$.Models;

namespace $rootnamespace$
{
    public class POXConfigurationProvider : IPOXConfigurationProvider
    {
        public POXConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/pox";
                string apiKey = "<<Your API key>>";
				int timeoutSeconds = 30;
                return new POXConfiguration(address, "$rootnamespace$", apiKey, timeoutSeconds);
            }
        }
    }
}
