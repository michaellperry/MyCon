using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Conferences.Models;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.Schedule.ViewModels;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Schedule.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private const string Environment = "Development";

        private readonly SynchronizationService _synchronizationService;
        private readonly ConferenceSelection _conferenceSelection;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();

            _conferenceSelection = new ConferenceSelection();
            _imageCache = new ImageCache();
            _searchModel = new SearchModel();
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

            return ForView.Wrap(new ScheduleViewModel(
                _synchronizationService,
                attendee,
                _imageCache,
                _searchModel));
        }

        public object GetSessionDetailsViewModel(string sessionId)
        {
            throw new NotImplementedException();
        }

        public object GetSessionEvaluationViewModel(string sessionId)
        {
            throw new NotImplementedException();
        }

        internal object GetSlotViewModel(string startTime)
        {
            throw new NotImplementedException();
        }

        internal object GetSpeakerViewModel(string speakerId)
        {
            throw new NotImplementedException();
        }
    }
}
