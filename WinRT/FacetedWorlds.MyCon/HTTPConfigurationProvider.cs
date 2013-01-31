using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/bin";
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
				int timeoutSeconds = 30;
                return new HTTPConfiguration(address, "FacetedWorlds.MyCon", apiKey, timeoutSeconds);
            }
        }

        public bool IsToastEnabled
        {
            get { return false; }
        }
    }
}
