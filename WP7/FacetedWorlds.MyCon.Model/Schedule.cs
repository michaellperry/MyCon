
namespace FacetedWorlds.MyCon.Model
{
    public partial class Schedule
    {
        public SessionEvaluation CreateEvaluation()
        {
            if (SessionPlace.Session.Conference.SessionSurvey.InConflict)
                return null;
            Survey survey = SessionPlace.Session.Conference.SessionSurvey;
            if (survey == null)
                return null;
            return Community.AddFact(new SessionEvaluation(this, survey));
        }
    }
}
