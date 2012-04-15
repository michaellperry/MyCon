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
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ConferenceViewModel
    {
        private readonly Conference _conference;
        private readonly NavigationModel _navigationModel;
        private readonly SurveySnapshotModel _surveySnapshot;
        private readonly SurveyNavigationModel _surveyNavigationModel;
        
        public ConferenceViewModel(Conference conference, NavigationModel navigationModel, SurveySnapshotModel surveySnapshot, SurveyNavigationModel surveyNavigationModel)
        {
            _conference = conference;
            _navigationModel = navigationModel;
            _surveySnapshot = surveySnapshot;
            _surveyNavigationModel = surveyNavigationModel;
        }

        public string Name
        {
            get { return _conference.Name; }
            set { _conference.Name = value; }
        }

        public string MapUrl
        {
            get { return _conference.MapUrl; }
            set { _conference.MapUrl = value; }
        }

        public SurveyReadOnlyViewModel SurveyReadOnly
        {
            get
            {
                Survey survey = _conference.CurrentSessionSurveys.Select(p => p.SessionSurvey).FirstOrDefault();
                return survey == null
                    ? null
                    : new SurveyReadOnlyViewModel(survey);
            }
        }

        public SurveyEditViewModel SurveyEdit
        {
            get
            {
                return new SurveyEditViewModel(_surveySnapshot, _surveyNavigationModel, () =>
                {
                    _conference.NewSurvey(
                        _surveySnapshot.RatingQuestions.ToList(),
                        _surveySnapshot.EssayQuestions.ToList());
                });
            }
        }

        public ICommand EditSurvey
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        _surveySnapshot.Set(_conference.CurrentSessionSurveys
                            .Select(p => p.SessionSurvey)
                            .FirstOrDefault());
                    });
            }
        }

        public IEnumerable<string> Tracks
        {
            get
            {
                return
                    from track in _conference.AllTracks
                    orderby track.Name
                    select track.Name;
            }
        }

        public string NewTrackName
        {
            get { return _navigationModel.NewTrackName; }
            set { _navigationModel.NewTrackName = value; }
        }

        public ICommand NewTrack
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_navigationModel.NewTrackName))
                    .Do(() => _conference.NewTrack(_navigationModel.NewTrackName));
            }
        }
    }
}
