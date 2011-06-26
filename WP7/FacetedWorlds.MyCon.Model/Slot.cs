using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Slot
    {
        public bool IsScheduled(SessionPlace sessionPlace)
        {
            return CurrentSchedules.Any(s => s.SessionPlace == sessionPlace);
        }

        public void AddSchedule(SessionPlace sessionPlace)
        {
            Community.AddFact(new Schedule(this, sessionPlace));
        }

        public void RemoveSchedule(SessionPlace sessionPlace)
        {
            foreach (Schedule schedule in CurrentSchedules
                .Where(s => s.SessionPlace == sessionPlace))
            {
                Community.AddFact(new ScheduleRemove(schedule));
            }
        }
    }
}
