using System;
using System.Collections.Generic;
using System.Windows.Input;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SurveyEditViewModel
    {
        private readonly SurveySnapshotModel _surveySnapshot;
        private readonly SurveyNavigationModel _surveyNavigationModel;
        private readonly Action _save;

        public SurveyEditViewModel(SurveySnapshotModel surveySnapshot, SurveyNavigationModel surveyNavigationModel, Action save)
        {
            _surveySnapshot = surveySnapshot;
            _surveyNavigationModel = surveyNavigationModel;
            _save = save;
        }

        public IEnumerable<string> RatingQuestions
        {
            get { return _surveySnapshot.RatingQuestions; }
        }

        public IEnumerable<string> EssayQuestions
        {
            get { return _surveySnapshot.EssayQuestions; }
        }

        public string SelectedRatingQuestion
        {
            get { return _surveyNavigationModel.SelectedRatingQuestion; }
            set { _surveyNavigationModel.SelectedRatingQuestion = value; }
        }

        public string SelectedEssayQuestion
        {
            get { return _surveyNavigationModel.SelectedEssayQuestion; }
            set { _surveyNavigationModel.SelectedEssayQuestion = value; }
        }

        public string NewQuestion
        {
            get { return _surveyNavigationModel.NewQuestion; }
            set { _surveyNavigationModel.NewQuestion = value; }
        }

        public ICommand AddRatingQuestion
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_surveyNavigationModel.NewQuestion))
                    .Do(() =>
                    {
                        _surveySnapshot.AddRatingQuestion(_surveyNavigationModel.NewQuestion);
                        _surveyNavigationModel.NewQuestion = null;
                    });
            }
        }

        public ICommand DeleteRatingQuestion
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_surveyNavigationModel.SelectedRatingQuestion))
                    .Do(() =>
                    {
                        _surveySnapshot.RemoveRatingQuestion(_surveyNavigationModel.SelectedRatingQuestion);
                        _surveyNavigationModel.SelectedRatingQuestion = null;
                    });
            }
        }

        public ICommand AddEssayQuestion
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_surveyNavigationModel.NewQuestion))
                    .Do(() =>
                    {
                        _surveySnapshot.AddEssayQuestion(_surveyNavigationModel.NewQuestion);
                        _surveyNavigationModel.NewQuestion = null;
                    });
            }
        }

        public ICommand DeleteEssayQuestion
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_surveyNavigationModel.SelectedEssayQuestion))
                    .Do(() =>
                    {
                        _surveySnapshot.RemoveEssayQuestion(_surveyNavigationModel.SelectedEssayQuestion);
                        _surveyNavigationModel.SelectedEssayQuestion = null;
                    });
            }
        }

        public ICommand Save
        {
            get
            {
                return MakeCommand.Do(_save);
            }
        }
    }
}
