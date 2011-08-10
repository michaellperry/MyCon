using System;
using UpdateControls.Fields;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Models
{
    public class NavigationModel
    {
        private Independent<Conference> _selectedConference = new Independent<Conference>();
        private Independent<string> _newRoomNumber = new Independent<string>();
        private Independent<string> _newSpeakerName = new Independent<string>();
        private Independent<Speaker> _selectedSpeaker = new Independent<Speaker>();
        private Independent<Session> _selectedSession = new Independent<Session>();
        private Independent<string> _newSessionId = new Independent<string>();
        private Independent<Track> _selectedTrack = new Independent<Track>();
        private Independent<string> _newTrackName = new Independent<string>();

        public Conference SelectedConference
        {
            get { return _selectedConference; }
            set { _selectedConference.Value = value; }
        }

        public string NewRoomNumber
        {
            get { return _newRoomNumber; }
            set { _newRoomNumber.Value = value; }
        }

        public string NewSpeakerName
        {
            get { return _newSpeakerName; }
            set { _newSpeakerName.Value = value; }
        }

        public Speaker SelectedSpeaker
        {
            get { return _selectedSpeaker; }
            set { _selectedSpeaker.Value = value; }
        }

        public Session SelectedSession
        {
            get { return _selectedSession; }
            set { _selectedSession.Value = value; }
        }

        public string NewSessionId
        {
            get { return _newSessionId; }
            set { _newSessionId.Value = value; }
        }

        public Track SelectedTrack
        {
            get { return _selectedTrack; }
            set { _selectedTrack.Value = value; }
        }

        public string NewTrackName
        {
            get { return _newTrackName; }
            set { _newTrackName.Value = value; }
        }
    }
}
