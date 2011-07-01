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
    }
}
