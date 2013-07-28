using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;
using FacetedWorlds.MyCon.Model;

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
                string apiKey = "<<Your API key>>";
                return new HTTPConfiguration(address, "FacetedWorlds.MyCon", apiKey);
            }
        }

        public bool IsToastEnabled
        {
            get { return false; }
        }
    }
}
