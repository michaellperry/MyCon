using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class SessionEvaluationEssay
    {
        public string Answer
        {
            get { return CurrentAnswers.Select(a => a.Value).FirstOrDefault(); }
            set { Community.AddFact(new SessionEvaluationEssayAnswer(this, CurrentAnswers, value)); }
        }
    }
}
