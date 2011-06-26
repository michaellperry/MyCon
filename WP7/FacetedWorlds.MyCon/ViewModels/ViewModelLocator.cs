using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;
        private readonly SettingsViewModel _settings;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();
            _main = new MainViewModel(_synchronizationService.Identity, _synchronizationService);
            _settings = new SettingsViewModel(_synchronizationService.Identity);
            if (!DesignerProperties.IsInDesignTool)
                CreateSampleData();
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Settings
        {
            get { return ForView.Wrap(_settings); }
        }

        private void CreateSampleData()
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference("Conference ID"));
            conference.Name = "Dallas TechFest";

            // General sessions
            conference.NewSessionPlace(
                "Lunch",
                "",
                null,
                null,
                new DateTime(2009, 11, 6, 12, 0, 0),
                "Dining Hall");

            // Cloud
            conference.NewSessionPlace(
                "Developing Your First Azure Service",
                "Jennifer Marsman",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Jennifer_Marsman.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 9, 0, 0),
                "A110");
            conference.NewSessionPlace(
                "Developer's Overview of SQL Azure",
                "Chris Koenig",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Chris_Koenig.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 10, 30, 0),
                "A110");
            conference.NewSessionPlace(
                "Salesforce.com / Force.com Cloud Platform",
                "Fadi Shami",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Fadi_Shami.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 13, 0, 0),
                "A110");
            conference.NewSessionPlace(
                "Azure storage for the relational database minded developer",
                "Dennis Palmer",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Dennis_Palmer.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 14, 30, 0),
                "A110");
            conference.NewSessionPlace(
                "Business Value from the Cloud",
                "David Walker",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/David_Walker.jpg",
                "Cloud",
                new DateTime(2009, 11, 6, 16, 0, 0),
                "A110");

            // Silverlight & WPF
            conference.NewSessionPlace(
                "What's new in Silverlight 3.0?",
                "Todd Anglin",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Todd_Anglin.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 9, 0, 0),
                "B120");
            conference.NewSessionPlace(
                "XAML Data Bound for Greatness!",
                "Michael Benkovich",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Michael_Benkovich.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 10, 30, 0),
                "B120");
            conference.NewSessionPlace(
                "Rich Islands of Functionality: Silverlight in ASP.NET",
                "Todd Anglin",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Todd_Anglin.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 13, 0, 0),
                "B120");
            conference.NewSessionPlace(
                "Expression Blend 3.0 and Sketchflow",
                "Michael Benkovich",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Michael_Benkovich.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 14, 30, 0),
                "B120");
            conference.NewSessionPlace(
                "Building LOB Applications in Silverlight",
                "Chris Koenig",
                "http://techfests.com/Tulsa/2009/SiteImages/Speakers/Chris_Koenig.jpg",
                "Silverlight & WPF",
                new DateTime(2009, 11, 6, 16, 0, 0),
                "B120");
        }
    }
}
