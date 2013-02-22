using System;
using System.Linq;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Individual
    {
        public async Task<Schedule> AddScheduleAsync(SessionPlace sessionPlace)
        {
            Attendee attendee = (await Attendees.EnsureAsync()).FirstOrDefault();
            if (attendee == null)
                attendee = await Community.AddFactAsync(new Attendee(
                    sessionPlace.Session.Conference,
                    Guid.NewGuid().ToString()));
            await Community.AddFactAsync(new IndividualAttendee(this, attendee));
            var slot = await Community.AddFactAsync(new Slot(
                attendee,
                sessionPlace.Place.PlaceTime));
            return await Community.AddFactAsync(new Schedule(slot, sessionPlace));
        }

        public async Task RemoveScheduleAsync(SessionPlace sessionPlace)
        {
            foreach (var attendee in await Attendees.EnsureAsync())
            {
                foreach (var schedule in await attendee.CurrentSchedules.EnsureAsync())
                {
                    if (schedule.SessionPlace == sessionPlace)
                        await Community.AddFactAsync(new ScheduleRemove(schedule));
                }
            }
        }

        public bool IsScheduled(SessionPlace sessionPlace)
        {
            var sessions =
                from attendee in Attendees
                from sp in attendee.ScheduledSessionPlaces
                select sp;
            return sessions.Contains(sessionPlace);
        }
    }
}
