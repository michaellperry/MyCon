using System;
using UpdateControls.Correspondence.POXClient;

namespace FacetedWorlds.MyCon.SurveyDump
{
    public class POXConfigurationProvider : IPOXConfigurationProvider
    {
        public bool IsToastEnabled
        {
            get { return false; }
        }

        public POXConfiguration Configuration
        {
            get
            {
                string endpoint = "https://api.facetedworlds.com/correspondence_server_web/pox";
                string channelName = "FacetedWorlds.MyCon.Capture";
                string apiKey = "A85666C6917C49528A5D608B82A0087E";
                return new POXConfiguration(endpoint, channelName, apiKey);
            }
        }
    }
}
