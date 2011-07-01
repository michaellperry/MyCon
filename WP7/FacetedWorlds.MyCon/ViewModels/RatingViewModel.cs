using System;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class RatingViewModel
    {
        private readonly SessionEvaluationRating _sessionEvaluationRating;

        public RatingViewModel(SessionEvaluationRating sessionEvaluationRating)
        {
            _sessionEvaluationRating = sessionEvaluationRating;
        }

        public string Question
        {
            get { return _sessionEvaluationRating.Question.Text; }
        }

        public bool OneStar
        {
            get { return _sessionEvaluationRating.Answer >= 1; }
        }

        public bool TwoStars
        {
            get { return _sessionEvaluationRating.Answer >= 2; }
        }

        public bool ThreeStars
        {
            get { return _sessionEvaluationRating.Answer >= 3; }
        }

        public bool FourStars
        {
            get { return _sessionEvaluationRating.Answer >= 4; }
        }

        public bool FiveStars
        {
            get { return _sessionEvaluationRating.Answer >= 5; }
        }
    }
}
