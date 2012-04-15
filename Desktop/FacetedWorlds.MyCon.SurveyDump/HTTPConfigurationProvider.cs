using UpdateControls.Correspondence.BinaryHTTPClient;

namespace FacetedWorlds.MyCon.SurveyDump
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string endpoint = "https://api.facetedworlds.com/correspondence_server_web/bin";
                string channelName = "FacetedWorlds.MyCon.Capture";
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
                return new HTTPConfiguration(endpoint, channelName, apiKey);
            }
        }
    }
}
