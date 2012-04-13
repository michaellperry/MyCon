using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;
using System.Configuration;

namespace FacetedWorlds.MyCon.Web
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string address = ConfigurationManager.AppSettings["CorrespondenceAddress"];
                string apiKey = ConfigurationManager.AppSettings["CorrespondenceAPIKey"];
                int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["CorrespondencePollingIntervalSeconds"]);
                return new HTTPConfiguration(address, "FacetedWorlds.MyCon.Web", apiKey, timeoutSeconds);
            }
        }
    }
}
