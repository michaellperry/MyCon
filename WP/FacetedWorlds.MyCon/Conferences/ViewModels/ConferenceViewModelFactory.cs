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
                var conference = DefineConference(
                    community,
                    catalog,
                    "That Conference 2013",
                    "http://www.thatconference.com/Images/mainLogo.png",
                    new DateTime(2013, 8, 16),
                    new DateTime(2013, 8, 19),
                    "Wisconsin Dells, WI");
                DefineConferenceSessions(community, conference);
            }


            Func<ConferenceHeader, ConferenceHeaderViewModel> makeConferenceHeaderViewModel =
                conferenceHeader =>
                    new ConferenceHeaderViewModel(conferenceHeader);

            return new ConferenceListViewModel(catalog, conferenceSelection, makeConferenceHeaderViewModel);
        }

        private static Conference DefineConference(ICommunity community, Catalog catalog, string name, string imageUrl, DateTime startDate, DateTime endDate, string city)
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
            return conference;
        }

        private static void DefineConferenceSessions(ICommunity community, Conference conference)
        {
            var developer = community.AddFact(new Track(conference));
            developer.Name = "Developer";

            var michael = community.AddFact(new Speaker(conference));
            michael.Name = "Michael L Perry";
            michael.ImageUrl = "http://qedcode.com/extras/Perry_Headshot_Medium.jpg";
            michael.Contact = "@michaellperry";
            michael.Bio.SetString(
                "Software is math. Michael L Perry has built upon the works of mathematicians like Bertrand Meyer, James Rumbaugh, and Donald Knuth to develop a mathematical system for software development. He has captured this system in a set of open source projects, Update Controls and Correspondence. As a Principal Consultant at Improving Enterprises, he applies mathematical concepts to building scalable and robust enterprise systems. You can find out more at qedcode.com.",
                v => michael.Bio = v,
                community);

            var provable = community.AddFact(new Session(michael));
            provable.Title = "4 Ways to Prevent Code Abuse";
            provable.Description.SetString(
                "Your code is right. Other people are just using it wrong!" +
                "Learn 4 simple techniques to prevent people from using your code incorrectly. We'll apply those techniques to a class in the .NET Framework that is really easy to get wrong. By the time we're done, you'll have to try really hard to mess it up." +
                "Some APIs will throw exceptions when you get something wrong. That's not helpful! I'll show you how to write an API that guides you toward correct code. It won't even compile unless you get it right." +
                "These 4 techniques are built into the C# language today, so take advantage of them! Everybody on your team will thank you. And you'll spend less time fixing their bugs.",
                v => provable.Description = v,
                community);

            community.AddFact(new SessionTrack(provable, developer, new List<SessionTrack>()));

            var eight = community.AddFact(new Time(conference));
            eight.StartTime = new DateTime(2013, 7, 12, 8, 0, 0, DateTimeKind.Local);

            var room100 = community.AddFact(new Room(conference));
            room100.RoomNumber = "100";

            var room100AtEight = community.AddFact(new Slot(eight, room100));

            community.AddFact(new SessionSlot(provable, room100AtEight, new List<SessionSlot>()));
        }
    }
}
