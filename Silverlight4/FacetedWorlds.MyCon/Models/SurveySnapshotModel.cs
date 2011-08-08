using System;
using System.Collections.Generic;
using System.Linq;
using UpdateControls.Collections;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Models
{
    public class SurveySnapshotModel
    {
        private IndependentList<string> _ratingQuestions = new IndependentList<string>();
        private IndependentList<string> _essayQuestions = new IndependentList<string>();

        public SurveySnapshotModel()
        {
        }

        public IEnumerable<string> RatingQuestions
        {
            get { return _ratingQuestions; }
        }

        public IEnumerable<string> EssayQuestions
        {
            get { return _essayQuestions; }
        }

        public void AddRatingQuestion(string question)
        {
            _ratingQuestions.Add(question);
        }

        public void RemoveRatingQuestion(string question)
        {
            _ratingQuestions.Remove(question);
        }

        public void AddEssayQuestion(string question)
        {
            _essayQuestions.Add(question);
        }

        public void RemoveEssayQuestion(string question)
        {
            _essayQuestions.Remove(question);
        }

        public void Set(Survey survey)
        {
            _ratingQuestions.Clear();
            _essayQuestions.Clear();

            if (survey != null)
            {
                foreach (string question in survey.RatingQuestions.Select(q => q.Text))
                {
                    _ratingQuestions.Add(question);
                }
                foreach (string question in survey.EssayQuestions.Select(q => q.Text))
                {
                    _essayQuestions.Add(question);
                }
            }
        }
    }
}
