using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Conferences.Models;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.MySchedule.ViewModels;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private const string Environment = "Development";

        private readonly SynchronizationService _synchronizationService;
        private readonly ConferenceSelection _conferenceSelection;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();

            _conferenceSelection = new ConferenceSelection();
        }

        public object Main
        {
            get
            {
                return ViewModel(() => _synchronizationService.Individual == null
                    ? null :
                    new MainViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Individual));
            }
        }

        public object ConferenceList
        {
            get
            {
                return ViewModel(() =>
                    ConferenceListViewModelFactory.Create(
                        Environment, 
                        _synchronizationService.Community, 
                        _conferenceSelection));
            }
        }

        public object GetConferenceDetailsViewModel(Guid conferenceId)
        {
            var catalog = _synchronizationService.Community.AddFact(new Catalog(Environment));
            var conferenceHeader = catalog.ConferenceHeaders.Ensure()
                .FirstOrDefault(c => c.Conference.Unique == conferenceId);
            if (conferenceHeader == null)
                return null;

            return ForView.Wrap(new ConferenceDetailsViewModel(
                _synchronizationService.Individual,
                conferenceHeader,
                _conferenceSelection));
        }

        public object GetScheduleViewModel(Guid conferenceId)
        {
            var attendee = _synchronizationService.Individual.ActiveAttendees.Ensure()
                .FirstOrDefault(a => a.Conference.Unique == conferenceId);
            if (attendee == null)
                return null;

            return ForView.Wrap(new ScheduleViewModel(attendee));
        }
    }
}
