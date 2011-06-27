using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;
        private readonly SettingsViewModel _settings;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();
            _imageCache = new ImageCache();
            _searchModel = new SearchModel();
            _main = new MainViewModel(_synchronizationService.Identity, _synchronizationService, _imageCache, _searchModel);
            _settings = new SettingsViewModel(_synchronizationService.Identity);
            if (!DesignerProperties.IsInDesignTool)
                CreateSampleData();
        }

        public SearchModel SearchModel
        {
            get { return _searchModel; }
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Settings
        {
            get { return ForView.Wrap(_settings); }
        }

        public object GetSessionDetailsViewModel(string sessionId)
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference("Conference ID"));
            Session session = conference.Sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return null;

            Attendee attendee = _synchronizationService.Community.AddFact(new Attendee(_synchronizationService.Identity, conference));
            if (session.CurrentSessionPlaces.Count() != 1)
                return null;

            SessionPlace sessionPlace = session.CurrentSessionPlaces.Single();
            Slot slot = attendee.NewSlot(sessionPlace.Place.PlaceTime);
            return ForView.Wrap(new SessionDetailsViewModel(slot, sessionPlace, _imageCache, _searchModel));
        }

        public object GetSlotViewModel(string startTime)
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference("Conference ID"));
            Attendee attendee = _synchronizationService.Community.AddFact(new Attendee(_synchronizationService.Identity, conference));
            DateTime start;
            if (!DateTime.TryParse(startTime, out start))
                return null;

            Day day = conference.Days.FirstOrDefault(d => d.ConferenceDate == start.Date);
            if (day == null)
                return null;

            Time time = day.Times.FirstOrDefault(t => t.Start == start);
            if (time == null)
                return null;

            Slot slot = attendee.NewSlot(time);
            return ForView.Wrap(new SlotViewModel(slot, _imageCache));
        }

        private void CreateSampleData()
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference("Conference ID"));
            conference.Name = "Dallas TechFest";

            // General sessions
            conference.NewSessionPlace(
                "lunch", 
                "Lunch", 
                "Vegetarian options available",
                String.Empty, 
                String.Empty,
                String.Empty,
                "http://qedcode.com/images/lunch.png", 
                null, 
                new DateTime(2009, 11, 6, 12, 0, 0), 
                "Dining Hall");

            // Cloud
            conference.NewSessionPlace(
                "cloud1",
                "Developing Your First Azure Service",
                "In this session we take a tour of the capabilities of the Microsoft cloud platform by building and running a simple service using the platform SDK. The sample service highlights some of the features of the platform including service management, storage, and an integrated developer experience. This is a demo-heavy session.",
                "Jennifer Marsman",
                "@jmarsman",
                "Jennifer Marsman is a Developer Evangelist in Microsoft’s Developer and Platform Evangelism group, where she educates developers on Microsoft’s new technologies.  Prior to becoming a Developer Evangelist, Jennifer was a software developer in Microsoft’s Natural Interactive Services division.  In this role, she filed two patents for her work in search and data mining algorithms.  Jennifer has also held positions with Ford Motor Company, National Instruments, and Soar Technology.\nJennifer earned a Bachelor’s Degree in Computer Engineering and Master’s Degree in Computer Science and Engineering from the University of Michigan in Ann Arbor.  Her graduate work specialized in artificial intelligence and computational theory.",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Jennifer_Marsman.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 9, 0, 0),
                "A110");
            conference.NewSessionPlace(
                "cloud2",
                "Developer's Overview of SQL Azure",
                "",
                "Chris Koenig",
                "@chriskoenig",
                "Chris Koenig is a Developer Evangelist with Microsoft, based in Dallas, TX. Prior to joining Microsoft, Chris worked as a Senior Architect on the Architecture Strategy Team for The Capital Group in San Antonio, and as an Architect, Developer and Development Team Lead for the global solution provider Avanade. As a consultant, Chris worked with a variety of clients from many vertical markets, ISVs and other solution providers on enterprise-class Windows and web-based applications. Today, Chris focuses on building, growing, and enhancing the developer communities in Texas, Oklahoma, Louisiana and Arkansas. Chris is a devoted husband and father of four awesome children who keep him very busy.  In his spare time, Chris serves as Scoutmaster for his oldest son's Troop, and Committee Chair for his youngest sons' Pack.  Chris also enjoys traveling, cooking, camping and playing guitar.  You can contact Chris through his blog at http://blogs.msdn.com/chkoenig, via email at chris.koenig@microsoft.com, or via Windows Live Messenger at chris@koenigweb.com.",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Chris_Koenig.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 10, 30, 0),
                "A110");
            conference.NewSessionPlace(
                "cloud3",
                "Salesforce.com / Force.com Cloud Platform",
                "",
                "Fadi Shami",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Fadi_Shami.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 13, 0, 0),
                "A110");
            conference.NewSessionPlace(
                "cloud4",
                "Azure storage for the relational database minded developer",
                "",
                "Dennis Palmer",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Dennis_Palmer.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 14, 30, 0),
                "A110");
            conference.NewSessionPlace(
                "cloud5",
                "Business Value from the Cloud",
                "",
                "David Walker",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/David_Walker.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 16, 0, 0),
                "A110");

            // Silverlight & WPF
            conference.NewSessionPlace(
                "xaml1",
                "What's new in Silverlight 3.0?",
                "",
                "Todd Anglin",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Todd_Anglin.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 9, 0, 0),
                "B120");
            conference.NewSessionPlace(
                "xaml2",
                "XAML Data Bound for Greatness!",
                "",
                "Michael Benkovich",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Michael_Benkovich.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 10, 30, 0),
                "B120");
            conference.NewSessionPlace(
                "xaml3",
                "Rich Islands of Functionality: Silverlight in ASP.NET",
                "",
                "Todd Anglin",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Todd_Anglin.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 13, 0, 0),
                "B120");
            conference.NewSessionPlace(
                "xaml4",
                "Expression Blend 3.0 and Sketchflow",
                "",
                "Michael Benkovich",
                "",
                "",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Michael_Benkovich.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 14, 30, 0),
                "B120");
            conference.NewSessionPlace(
                "xaml5",
                "Building LOB Applications in Silverlight",
                "",
                "Chris Koenig",
                "@chriskoenig",
                "Chris Koenig is a Developer Evangelist with Microsoft, based in Dallas, TX. Prior to joining Microsoft, Chris worked as a Senior Architect on the Architecture Strategy Team for The Capital Group in San Antonio, and as an Architect, Developer and Development Team Lead for the global solution provider Avanade. As a consultant, Chris worked with a variety of clients from many vertical markets, ISVs and other solution providers on enterprise-class Windows and web-based applications. Today, Chris focuses on building, growing, and enhancing the developer communities in Texas, Oklahoma, Louisiana and Arkansas. Chris is a devoted husband and father of four awesome children who keep him very busy.  In his spare time, Chris serves as Scoutmaster for his oldest son's Troop, and Committee Chair for his youngest sons' Pack.  Chris also enjoys traveling, cooking, camping and playing guitar.  You can contact Chris through his blog at http://blogs.msdn.com/chkoenig, via email at chris.koenig@microsoft.com, or via Windows Live Messenger at chris@koenigweb.com.",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Chris_Koenig.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 16, 0, 0),
                "B120");
        }
    }
}
