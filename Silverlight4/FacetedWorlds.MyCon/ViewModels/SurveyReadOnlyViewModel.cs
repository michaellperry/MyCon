using System;
using FacetedWorlds.MyCon.Model;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SurveyReadOnlyViewModel
    {
        private readonly Survey _survey;

        public SurveyReadOnlyViewModel(Survey survey)
        {
            _survey = survey;
        }

        public IEnumerable<string> RatingQuestions
        {
            get
            {
                return
                    from ratingQuestion in _survey.RatingQuestions
                    select ratingQuestion.Text;
            }
        }

        public IEnumerable<string> EssayQuestions
        {
            get
            {
                return
                    from essayQuestion in _survey.EssayQuestions
                    select essayQuestion.Text;
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            SurveyReadOnlyViewModel that = obj as SurveyReadOnlyViewModel;
            if (that == null)
                return false;
            return _survey.Equals(that._survey);
        }

        public override int GetHashCode()
        {
            return _survey.GetHashCode();
        }
    }
}
