using System;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Conference
    {
        public void NewSessionPlace(string sessionId, string sessionName, string speakerName, string imageUrl, string trackName, DateTime startTime, string roomNumber)
        {
            Speaker speaker = Community.AddFact(new Speaker(this, speakerName));
            if (speaker.ImageUrl.Value != imageUrl)
                speaker.ImageUrl = imageUrl;
            Track track = trackName == null ? null : Community.AddFact(new Track(this, trackName));
            Session session = Community.AddFact(new Session(this, speaker, track, sessionId));
            if (session.Name.Value != sessionName)
                session.Name = sessionName;
            Day day = Community.AddFact(new Day(this, startTime.Date));
            Time time = Community.AddFact(new Time(day, startTime));
            Room room = Community.AddFact(new Room(this, roomNumber));
            Place place = Community.AddFact(new Place(time, room));
            Community.AddFact(new SessionPlace(session, place, new List<SessionPlace>()));
        }
    }
}
