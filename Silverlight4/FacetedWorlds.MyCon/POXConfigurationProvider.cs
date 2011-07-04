using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.POXClient;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon
{
    public class POXConfigurationProvider : IPOXConfigurationProvider
    {
        public POXConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/pox";
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
				int timeoutSeconds = 30;
                return new POXConfiguration(address, "FacetedWorlds.MyCon", apiKey, timeoutSeconds);
            }
        }
    }
}
