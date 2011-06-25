using System;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Conference
    {
        public void NewSessionPlace(string sessionName, string speakerName, string trackName, DateTime startTime, string roomNumber)
        {
            Speaker speaker = Community.AddFact(new Speaker(this, speakerName));
            Track track = trackName == null ? null : Community.AddFact(new Track(this, trackName));
            Session session = Sessions.FirstOrDefault(s => s.Name == sessionName && s.Speaker == speaker && s.Track == track);
            if (session == null)
            {
                session = Community.AddFact(new Session(this, speaker, track));
                session.Name = sessionName;
            }
            Day day = Community.AddFact(new Day(this, startTime.Date));
            Time time = Community.AddFact(new Time(day, startTime));
            Room room = Community.AddFact(new Room(this, roomNumber));
            Place place = Community.AddFact(new Place(time, room));
            Community.AddFact(new SessionPlace(session, place, new List<SessionPlace>()));
        }
    }
}
