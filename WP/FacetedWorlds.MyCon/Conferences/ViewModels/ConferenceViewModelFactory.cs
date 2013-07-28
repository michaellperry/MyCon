using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceListViewModelFactory
    {
        private const string Environment = "Development";
        private const bool LoadSampleData = true;

        public static ConferenceListViewModel Create(ICommunity community)
        {
            var catalog = community.AddFact(new Catalog(Environment));

            if (LoadSampleData && !catalog.ConferenceHeaders.Ensure().Any())
            {
                DefineConference(
                    community,
                    catalog,
                    "Cowtown Code Camp 2013",
                    "http://cdn.marketplaceimages.windowsphone.com/v8/images/2a24b5e4-d79a-448e-8818-4040ff0d51a6?imageType=ws_icon_large",
                    new DateTime(2013, 3, 17),
                    "Fort Worth, TX");
                DefineConference(
                    community,
                    catalog,
                    "AwesomeFest 2014",
                    "http://files.g4tv.com/ImageDb3/284724_S/darth-vader-rock-guitarist.jpg",
                    new DateTime(2014, 2, 27),
                    "Allen, TX");
                DefineConference(
                    community,
                    catalog,
                    "That Conference 2013",
                    "http://www.thatconference.com/Images/mainLogo.png",
                    new DateTime(2013, 8, 16),
                    "Wisconsin Dells, WI");
            }

            Func<ConferenceHeader, ConferenceHeaderViewModel> makeConferenceHeaderViewModel =
                conferenceHeader =>
                    new ConferenceHeaderViewModel(conferenceHeader);

            return new ConferenceListViewModel(catalog, makeConferenceHeaderViewModel);
        }

        private static void DefineConference(ICommunity community, Catalog catalog, string name, string imageUrl, DateTime date, string location)
        {
            var conference = community.AddFact(new Conference());
            var header = community.AddFact(new ConferenceHeader(catalog, conference));
            header.Name = name;
            header.ImageUrl = imageUrl;
            header.StartDate = date;
            header.Location = location;
        }
    }
}
