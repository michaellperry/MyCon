using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        private Independent<Individual> _individual = new Independent<Individual>();

        public Individual Individual
        {
            get { return _individual; }
            set { _individual.Value = value; }
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
            get { return Individual == null ? false : Individual.ToastNotificationEnabled; }
        }
    }
}
