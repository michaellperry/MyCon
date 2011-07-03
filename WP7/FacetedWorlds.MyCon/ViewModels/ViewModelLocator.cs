using System;
using System.Collections.Generic;
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
        private const string ConferenceID = "Conference ID";

        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;
        private readonly SettingsViewModel _settings;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;
        private readonly Clock _clock;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();
            _imageCache = new ImageCache();
            _searchModel = new SearchModel();
            _clock = new Clock();

            _main = new MainViewModel(_synchronizationService.Identity, _synchronizationService, _imageCache, _searchModel, _clock);
            _settings = new SettingsViewModel(_synchronizationService.Identity);
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
            Conference conference = _synchronizationService.Community.AddFact(new Conference(ConferenceID));
            Session session = conference.Sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return null;

            Attendee attendee = _synchronizationService.Community.AddFact(new Attendee(_synchronizationService.Identity, conference));
            if (session.CurrentSessionPlaces.Count() != 1)
                return null;

            SessionPlace sessionPlace = session.CurrentSessionPlaces.Single();
            Slot slot = attendee.NewSlot(sessionPlace.Place.PlaceTime);
            return ForView.Wrap(new SessionDetailsViewModel(slot, sessionPlace, _imageCache, _searchModel, _clock));
        }

        public object GetSlotViewModel(string startTime)
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference(ConferenceID));
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

        public object GetSpeakerViewModel(string speakerId)
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference(ConferenceID));
            Attendee attendee = _synchronizationService.Community.AddFact(new Attendee(_synchronizationService.Identity, conference));
            Speaker speaker = conference.Speakers.FirstOrDefault(s => s.Name == speakerId);
            if (speaker == null)
                return null;

            return ForView.Wrap(new SpeakerViewModel(attendee, speaker, _imageCache));
        }

        public object GetSessionEvaluationViewModel(string sessionId)
        {
            Conference conference = _synchronizationService.Community.AddFact(new Conference(ConferenceID));
            List<Session> sessions = conference.Sessions.Where(s => s.Id == sessionId).ToList();
            if (sessions.Count != 1)
                return null;

            Session session = sessions[0];
            Attendee attendee = _synchronizationService.Community.AddFact(new Attendee(_synchronizationService.Identity, conference));
            if (session.CurrentSessionPlaces.Count() != 1)
                return null;

            List<Schedule> schedules = attendee.CurrentSchedules.Where(s => s.SessionPlace.Session.Id == sessionId).ToList();
            if (schedules.Count != 1)
                return null;

            Schedule schedule = schedules[0];
            SessionEvaluation sessionEvaluation = schedule.CreateEvaluation();
            if (sessionEvaluation == null)
                return null;

            return ForView.Wrap(new SessionEvaluationViewModel(sessionEvaluation, _imageCache));
        }
    }
}
