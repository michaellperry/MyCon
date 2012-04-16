using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;

namespace $rootnamespace$
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/bin";
                string apiKey = "<<Your API key>>";
				int timeoutSeconds = 30;
                return new HTTPConfiguration(address, "$rootnamespace$", apiKey, timeoutSeconds);
            }
        }
    }
}
