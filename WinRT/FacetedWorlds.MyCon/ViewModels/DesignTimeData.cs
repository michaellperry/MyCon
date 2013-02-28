using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class DesignTimeData
    {
        public static async Task Populate(Community community, Conference conference, Individual individual)
        {
            conference.Name = "AwesomeFest 2013: The Gathering";

            var day = await community.AddFactAsync(new Day(conference, new DateTime(2013, 2, 23)));
            var placeTime = await community.AddFactAsync(new Time(day, new DateTime(2013, 2, 23, 8, 0, 0)));
            var room = await community.AddFactAsync(new Room(conference));
            room.RoomNumber = "101";
            var place = await community.AddFactAsync(new Place(placeTime, room));
            var speaker = await community.AddFactAsync(new Speaker(conference, "Speaker One"));
            var track = await community.AddFactAsync(new Track(conference, "Agile"));
            var session = await community.AddFactAsync(new Model.Session(conference, speaker, track));
            session.Name = "Kanban, Planning Poker, and Other Crazy Practices";
            var sessionPlace = await community.AddFactAsync(new SessionPlace(session, place, Enumerable.Empty<SessionPlace>()));
        }
    }
}
