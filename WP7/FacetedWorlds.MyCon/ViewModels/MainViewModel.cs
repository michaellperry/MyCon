using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private Identity _identity;
        private SynchronizationService _synhronizationService;

        public MainViewModel(Identity identity, SynchronizationService synhronizationService)
        {
            _identity = identity;
            _synhronizationService = synhronizationService;

            CreateSampleData();
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }

        public ScheduleViewModel Schedule
        {
            get { return new ScheduleViewModel(_identity.NewAttendee("Conference ID")); }
        }

        private void CreateSampleData()
        {
            var attendee = _identity.NewAttendee("Conference ID");
            attendee.Conference.Name = "Dallas TechFest";
            attendee.Conference.NewSessionPlace(
                "Developing Your First Azure Service",
                "Jennifer Marsman",
                "Cloud",
                new DateTime(2009, 11, 6, 9, 0, 0),
                "A110");
            attendee.Conference.NewSessionPlace(
                "Developer's Overview of SQL Azure",
                "Chris Koenig",
                "Cloud",
                new DateTime(2009, 11, 6, 10, 30, 0),
                "A110");
            attendee.Conference.NewSessionPlace(
                "Salesforce.com / Force.com Cloud Platform",
                "Fadi Shami",
                "Cloud",
                new DateTime(2009, 11, 6, 13, 0, 0),
                "A110");
            attendee.Conference.NewSessionPlace(
                "Azure storage for the relational database minded developer",
                "Dennis Palmer",
                "Cloud",
                new DateTime(2009, 11, 6, 14, 30, 0),
                "A110");
            attendee.Conference.NewSessionPlace(
                "Business Value from the Cloud",
                "David Walker",
                "Cloud",
                new DateTime(2009, 11, 6, 16, 0, 0),
                "A110");
        }
    }
}
