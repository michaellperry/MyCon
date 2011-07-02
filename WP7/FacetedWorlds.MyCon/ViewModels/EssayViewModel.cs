using System;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class EssayViewModel
    {
        private readonly SessionEvaluationEssay _sessionEvaluationEssay;

        public EssayViewModel(SessionEvaluationEssay sessionEvaluationEssay)
        {
            _sessionEvaluationEssay = sessionEvaluationEssay;
        }

        public string Question
        {
            get { return _sessionEvaluationEssay.Question.Text; }
        }

        public string Answer
        {
            get { return _sessionEvaluationEssay.Answer; }
            set { _sessionEvaluationEssay.Answer = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            EssayViewModel that = obj as EssayViewModel;
            if (that == null)
                return false;
            return _sessionEvaluationEssay.Equals(that._sessionEvaluationEssay);
        }

        public override int GetHashCode()
        {
            return _sessionEvaluationEssay.GetHashCode();
        }
    }
}
