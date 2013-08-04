using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Conferences.Models;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceListViewModelFactory
    {
        private const bool LoadSampleData = true;

        public static ConferenceListViewModel Create(string environment, ICommunity community, ConferenceSelection conferenceSelection)
        {
            var catalog = community.AddFact(new Catalog(environment));

            if (LoadSampleData && !catalog.ConferenceHeaders.Ensure().Any())
            {
                DefineConference(
                    community,
                    catalog,
                    "Cowtown Code Camp 2013",
                    "http://cdn.marketplaceimages.windowsphone.com/v8/images/2a24b5e4-d79a-448e-8818-4040ff0d51a6?imageType=ws_icon_large",
                    new DateTime(2013, 3, 17),
                    new DateTime(2013, 3, 17),
                    "Fort Worth, TX");
                DefineConference(
                    community,
                    catalog,
                    "AwesomeFest 2014",
                    "http://files.g4tv.com/ImageDb3/284724_S/darth-vader-rock-guitarist.jpg",
                    new DateTime(2014, 2, 27),
                    new DateTime(2014, 2, 28),
                    "Allen, TX");
                DefineConference(
                    community,
                    catalog,
                    "That Conference 2013",
                    "http://www.thatconference.com/Images/mainLogo.png",
                    new DateTime(2013, 8, 16),
                    new DateTime(2013, 8, 19),
                    "Wisconsin Dells, WI");
            }


            Func<ConferenceHeader, ConferenceHeaderViewModel> makeConferenceHeaderViewModel =
                conferenceHeader =>
                    new ConferenceHeaderViewModel(conferenceHeader);

            return new ConferenceListViewModel(catalog, conferenceSelection, makeConferenceHeaderViewModel);
        }

        private static void DefineConference(ICommunity community, Catalog catalog, string name, string imageUrl, DateTime startDate, DateTime endDate, string city)
        {
            var conference = community.AddFact(new Conference());
            var header = community.AddFact(new ConferenceHeader(catalog, conference));
            header.Name = name;
            header.ImageUrl = imageUrl;
            header.StartDate = startDate;
            header.EndDate = endDate;
            header.City = city;
            header.Address = "111 Main Street";
            header.HomePageUrl = "awesomefest.com";
            header.Description.SetString(
                "Awesomeness!! Go to AwesomeFest!!!",
                value => header.Description = value,
                community);
        }
    }
}
