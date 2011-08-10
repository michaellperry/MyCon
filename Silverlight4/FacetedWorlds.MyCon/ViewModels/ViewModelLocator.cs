using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator
    {
        private readonly NavigationModel _navigationModel;
        private readonly SurveySnapshotModel _surveySnapshot;
        private readonly SurveyNavigationModel _surveyNavigationModel;
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            _navigationModel = new NavigationModel();
            _surveySnapshot = new SurveySnapshotModel();
            _surveyNavigationModel = new SurveyNavigationModel();
            _synchronizationService = new SynchronizationService(_navigationModel);
            if (!DesignerProperties.IsInDesignTool)
            {
                _synchronizationService.Initialize();
                TemporarilyPreselectDallasTechFest();
            }
            _main = new MainViewModel(_synchronizationService.Community, _navigationModel, _synchronizationService);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Conference
        {
            get { return ForView.Wrap(new ConferenceViewModel(_navigationModel.SelectedConference, _navigationModel, _surveySnapshot, _surveyNavigationModel)); }
        }

        public object Speakers
        {
            get { return ForView.Wrap(new SpeakersViewModel(_navigationModel.SelectedConference, _navigationModel)); }
        }

        public object Sessions
        {
            get
            {
                return ForView.Wrap(new SessionsViewModel(_navigationModel.SelectedConference, _navigationModel));
            }
        }

        public object Schedule
        {
            get { return ForView.Wrap(new ScheduleViewModel(_navigationModel.SelectedConference, _navigationModel)); }
        }

        private void TemporarilyPreselectDallasTechFest()
        {
            _navigationModel.SelectedConference = _synchronizationService.Conference;
        }
    }
}
