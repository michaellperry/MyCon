
namespace FacetedWorlds.MyCon.Model
{
    public partial class SessionEvaluation
    {
        public SessionEvaluationRating Rating(RatingQuestion question)
        {
            return Community.AddFact(new SessionEvaluationRating(this, question));
        }

        public SessionEvaluationEssay Essay(EssayQuestion question)
        {
            return Community.AddFact(new SessionEvaluationEssay(this, question));
        }
    }
}
