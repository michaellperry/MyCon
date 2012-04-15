using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using System.Collections.Generic;
using System.Linq;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionsViewModel
    {
        private readonly Conference _conference;
        private readonly NavigationModel _navigationModel;

        public SessionsViewModel(Conference conference, NavigationModel navigationModel)
        {
            _conference = conference;
            _navigationModel = navigationModel;
        }

        public IEnumerable<SessionViewModel> Sessions
        {
            get
            {
                return
                    from session in _conference.Sessions
                    orderby session.Name.Value
                    select new SessionViewModel(session);
            }
        }

        public SessionViewModel SelectedSession
        {
            get
            {
                return _navigationModel.SelectedSession == null
                    ? null
                    : new SessionViewModel(_navigationModel.SelectedSession);
            }
            set
            {
                _navigationModel.SelectedSession = value == null
                    ? null
                    : value.Session;
            }
        }

        public string NewSessionId
        {
            get { return _navigationModel.NewSessionId; }
            set { _navigationModel.NewSessionId = value; }
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _conference.Tracks
                    orderby track.Name
                    select new TrackViewModel(track);
            }
        }

        public TrackViewModel SelectedTrack
        {
            get
            {
                return _navigationModel.SelectedTrack == null
                    ? null
                    : new TrackViewModel(_navigationModel.SelectedTrack);
            }
            set
            {
                _navigationModel.SelectedTrack = value == null
                    ? null
                    : value.Track;
            }
        }

        public IEnumerable<SpeakerViewModel> Speakers
        {
            get
            {
                return
                    from speaker in _conference.Speakers
                    orderby speaker.Name
                    select new SpeakerViewModel(speaker);
            }
        }

        public SpeakerViewModel SelectedSpeaker
        {
            get
            {
                return _navigationModel.SelectedSpeaker == null
                    ? null
                    : new SpeakerViewModel(_navigationModel.SelectedSpeaker);
            }
            set
            {
            	_navigationModel.SelectedSpeaker = value == null
                    ? null
                    : value.Speaker;
            }
        }

        public ICommand NewSession
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_navigationModel.NewSessionId) && _navigationModel.SelectedSpeaker != null)
                    .Do(() =>
                    {
                        _navigationModel.SelectedSession = _conference.NewSession(_navigationModel.SelectedSpeaker, _navigationModel.SelectedTrack);
                        _navigationModel.NewSessionId = string.Empty;
                        _navigationModel.SelectedSpeaker = null;
                        _navigationModel.SelectedTrack = null;
                    });
            }
        }

        public SessionDetailsViewModel SessionDetails
        {
            get
            {
                return _navigationModel.SelectedSession == null
                    ? null
                    : new SessionDetailsViewModel(_navigationModel.SelectedSession);
            }
        }
    }
}
