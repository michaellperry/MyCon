using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class SessionEvaluationRating
    {
        public int Answer
        {
            get { return CurrentAnswers.Select(a => a.Value).FirstOrDefault(); }
            set { Community.AddFact(new SessionEvaluationRatingAnswer(this, CurrentAnswers, value)); }
        }
    }
}
