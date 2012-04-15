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
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;
        private readonly ScheduleViewModel _schedule;
        private readonly TracksViewModel _tracks;
        private readonly SearchViewModel _search;
        private readonly MapViewModel _map;
        private readonly NoticesViewModel _notices;
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

            _main = new MainViewModel(_synchronizationService.Attendee, _synchronizationService, _imageCache, _searchModel, _clock);
            _schedule = new ScheduleViewModel(_synchronizationService, _synchronizationService.Attendee, _imageCache, _searchModel);
            _tracks = new TracksViewModel(_synchronizationService.Attendee, _imageCache, _searchModel);
            _search = new SearchViewModel(_synchronizationService.Attendee, _imageCache, _searchModel);
            _map = new MapViewModel(_synchronizationService.Attendee, _imageCache, _clock);
            _notices = new NoticesViewModel(_synchronizationService.Attendee, _imageCache, _clock);
            _settings = new SettingsViewModel(_synchronizationService.Attendee);
        }

        public SearchModel SearchModel
        {
            get { return _searchModel; }
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Schedule
        {
            get { return ForView.Wrap(_schedule); }
        }

        public object Tracks
        {
            get { return ForView.Wrap(_tracks); }
        }

        public object Search
        {
            get { return ForView.Wrap(_search); }
        }

        public object Map
        {
            get { return ForView.Wrap(_map); }
        }

        public object Notices
        {
            get { return ForView.Wrap(_notices); }
        }

        public object Settings
        {
            get { return ForView.Wrap(_settings); }
        }

        public object GetSessionDetailsViewModel(string sessionId)
        {
            Attendee attendee = _synchronizationService.Attendee;
            Guid sessionGuid = new Guid(sessionId);
            Session session = attendee.Conference.Sessions.FirstOrDefault(s => s.Unique == sessionGuid);
            if (session == null)
                return null;
            if (session.CurrentSessionPlaces.Count() != 1)
                return null;

            SessionPlace sessionPlace = session.CurrentSessionPlaces.Single();
            Slot slot = attendee.NewSlot(sessionPlace.Place.PlaceTime);
            return ForView.Wrap(new SessionDetailsViewModel(slot, sessionPlace, _imageCache, _searchModel, _clock));
        }

        public object GetSlotViewModel(string startTime)
        {
            Attendee attendee = _synchronizationService.Attendee;
            DateTime start;
            if (!DateTime.TryParse(startTime, out start))
                return null;

            Day day = attendee.Conference.Days.FirstOrDefault(d => d.ConferenceDate == start.Date);
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
            Attendee attendee = _synchronizationService.Attendee;
            Speaker speaker = attendee.Conference.Speakers.FirstOrDefault(s => s.Name == speakerId);
            if (speaker == null)
                return null;

            return ForView.Wrap(new SpeakerViewModel(attendee, speaker, _imageCache));
        }

        public object GetSessionEvaluationViewModel(string sessionId)
        {
            Guid sessionGuid = new Guid(sessionId);
            Attendee attendee = _synchronizationService.Attendee;
            List<Session> sessions = attendee.Conference.Sessions.Where(s => s.Unique == sessionGuid).ToList();
            if (sessions.Count != 1)
                return null;

            Session session = sessions[0];
            if (session.CurrentSessionPlaces.Count() != 1)
                return null;

            List<Schedule> schedules = attendee.CurrentSchedules.Where(s => s.SessionPlace.Session.Unique == sessionGuid).ToList();
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
