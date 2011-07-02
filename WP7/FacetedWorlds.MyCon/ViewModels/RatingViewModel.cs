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

        public int Rating
        {
            get { return _sessionEvaluationRating.Answer; }
            set { _sessionEvaluationRating.Answer = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            RatingViewModel that = obj as RatingViewModel;
            if (that == null)
                return false;
            return _sessionEvaluationRating == that._sessionEvaluationRating;
        }

        public override int GetHashCode()
        {
            return _sessionEvaluationRating.GetHashCode();
        }
    }
}
