using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SpeakersViewModel
    {
        private readonly Conference _conference;
        private readonly NavigationModel _navigationModel;

        public SpeakersViewModel(Conference conference, NavigationModel navigationModel)
        {
            _conference = conference;
            _navigationModel = navigationModel;
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

        public SpeakerDetailsViewModel SpeakerDetails
        {
            get
            {
                return _navigationModel.SelectedSpeaker == null
                    ? null
                    : new SpeakerDetailsViewModel(_navigationModel.SelectedSpeaker);
            }
        }

        public string NewSpeakerName
        {
            get { return _navigationModel.NewSpeakerName; }
            set { _navigationModel.NewSpeakerName = value; }
        }

        public ICommand NewSpeaker
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_navigationModel.NewSpeakerName))
                    .Do(() =>
                    {
                        _navigationModel.SelectedSpeaker = _conference.GetSpeaker(_navigationModel.NewSpeakerName);
                        _navigationModel.NewSpeakerName = string.Empty;
                    });
            }
        }
    }
}
