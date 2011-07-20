using System.Linq;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Schedule
    {
        public SessionEvaluation CreateEvaluation()
        {
            List<ConferenceSessionSurvey> current = SessionPlace.Session.Conference.CurrentSessionSurveys.ToList();
            if (current.Count != 1)
                return null;
            Survey survey = current.Single().SessionSurvey;
            if (survey == null)
                return null;
            return Community.AddFact(new SessionEvaluation(this, survey));
        }
    }
}
