﻿using System;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionDetailsViewModel
    {
        private readonly Slot _slot;
        private readonly SessionPlace _sessionPlace;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;
        private readonly Clock _clock;

        public SessionDetailsViewModel(Slot slot, SessionPlace sessionPlace, ImageCache imageCache, SearchModel searchModel, Clock clock)
        {
            _slot = slot;
            _sessionPlace = sessionPlace;
            _imageCache = imageCache;
            _searchModel = searchModel;
            _clock = clock;
        }

        public string Time
        {
            get
            {
                return String.Format(
                    "{0:dddd, MMMM d} at {1:h:mm tt}",
                    _sessionPlace.Place.PlaceTime.Day.ConferenceDate.ToLocalTime(),
                    _sessionPlace.Place.PlaceTime.Start.ToLocalTime());
            }
        }

        public CachedImage ImageUrl
        {
            get { return _imageCache.LargeImageUrl(_sessionPlace.Session.Speaker.ImageUrl); }
        }

        public string Title
        {
            get { return _sessionPlace.Session.Name; }
        }

        public string Instructions
        {
            get
            {
                if (IsGeneralSession)
                {
                    return "This is a general session.";
                }
                else if (SessionIsScheduled)
                {
                    if (SessionHasStarted)
                    {
                        if (EvalIsCompleted)
                            return "Thank you for evaluating this session.";
                        else
                            return "Please fill out your session evaluation.";
                    }
                    else
                    {
                        if (Overbooked)
                            return "You are overbooked for this time. Tap X to remove.";
                        else
                            return "You are scheduled for this session. Tap X to remove.";
                    }
                }
                else
                {
                    if (Booked)
                        return "You already have a session scheduled for this time.";
                    else
                        return "Tap the plus to add this session to your schedule.";
                }
            }
        }

        public string Speaker
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public string Room
        {
            get { return _sessionPlace.Place.Room.RoomNumber; }
        }

        public string Track
        {
            get
            {
                Track track = _sessionPlace.Session.Track;
                return track.IsNull ? String.Empty : String.Format("Track: {0}", track.Name);
            }
        }

        public string Level
        {
            get
            {
                Level level = _sessionPlace.Session.Level.Value;
                return level == null ? String.Empty : String.Format("Level: {0}", level.Name);
            }
        }

        public bool Scheduled
        {
            get { return _slot.IsScheduled(_sessionPlace); }
        }

        private bool Booked
        {
            get { return _slot.CurrentSchedules.Any(); }
        }

        public bool Overbooked
        {
            get { return _slot.CurrentSchedules.Count() > 1; }
        }

        public string Description
        {
            get { return _sessionPlace.Session.Description.Value.JoinSegments(); }
        }

        public string Contact
        {
            get { return _sessionPlace.Session.Speaker.Contact; }
        }

        public string Bio
        {
            get { return _sessionPlace.Session.Speaker.Bio.Value.JoinSegments(); }
        }

        public bool CanAdd
        {
            get { return !_sessionPlace.Session.Track.IsNull && !_slot.IsScheduled(_sessionPlace); }
        }

        public void Add()
        {
            _slot.AddSchedule(_sessionPlace);
        }

        public bool ShouldPromptForPushNotification()
        {
            return _slot.Attendee.AllSchedules.Count() == 3;
        }

        public string GetConferenceName()
        {
            return _sessionPlace.Session.Conference.Name;
        }

        public bool CanRemove
        {
            get { return SessionIsScheduled && !SessionHasStarted; }
        }

        public void Remove()
        {
            _slot.RemoveSchedule(_sessionPlace);
        }

        public bool CanEvaluate
        {
            get { return !IsGeneralSession && SessionIsScheduled && SessionHasStarted && !EvalIsCompleted; }
        }

        public string SearchBySpeakerText
        {
            get
            {
                string speakerName = _sessionPlace.Session.Speaker.Name;
                return String.IsNullOrEmpty(speakerName)
                    ? "Other sessions by speaker"
                    : String.Format("Other sessions by {0}", speakerName);
            }
        }

        public bool CanSearchBySpeaker
        {
            get { return !String.IsNullOrEmpty(_sessionPlace.Session.Speaker.Name); }
        }

        public string SpeakerId
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public string SearchByTrackText
        {
            get
            {
                return _sessionPlace.Session.Track.IsNull
                    ? "Other sessions in track"
                    : String.Format("Other sessions in {0}", _sessionPlace.Session.Track.Name);
            }
        }

        public bool CanSearchByTrack
        {
            get { return !_sessionPlace.Session.Track.IsNull; }
        }

        public void SearchByTrack()
        {
            if (!_sessionPlace.Session.Track.IsNull)
                _searchModel.SelectedTrack = _sessionPlace.Session.Track.Name;
        }

        public string SearchByTimeText
        {
            get
            {
                return _sessionPlace.Session.Track.IsNull
                    ? "Other sessions at this time"
                    : String.Format("Other sessions at {0:h:mm}", _sessionPlace.Place.PlaceTime.Start.ToLocalTime());
            }
        }

        public bool CanSearchByTime
        {
            get { return true; }
        }

        public string SearchByTimeUri
        {
            get { return String.Format("/Views/SlotView.xaml?StartTime={0}", _slot.SlotTime.Start); }
        }

        public string SessionEvaluationUri
        {
            get { return String.Format("/Views/SessionEvaluationView.xaml?SessionId={0}", _sessionPlace.Session.Unique); }
        }

        private bool IsGeneralSession
        {
            get { return _sessionPlace.Session.Track.IsNull; }
        }

        private bool SessionIsScheduled
        {
            get { return _slot.IsScheduled(_sessionPlace); }
        }

        private bool SessionHasStarted
        {
            get { return _sessionPlace.Place.PlaceTime.Start.ToLocalTime().AddMinutes(30.0) < _clock.Time; }
        }

        private bool EvalIsCompleted
        {
            get
            {
                var evals =
                    from currentSchedule in _slot.CurrentSchedules
                    where currentSchedule.SessionPlace == _sessionPlace
                    from eval in currentSchedule.CompletedEvaluations
                    select eval;
                return evals.Any();
            }
        }
    }
}
