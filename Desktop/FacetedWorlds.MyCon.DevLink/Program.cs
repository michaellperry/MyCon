using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.DevLink.DevLinkServiceReference;
using System.Threading;
using System.Text.RegularExpressions;

namespace FacetedWorlds.MyCon.DevLink
{
    class Program
    {
        private static devlink2011Entities _entities;

        static void Main(string[] args)
        {
            _entities = new devlink2011Entities(new Uri("http://devlink.us/odata.svc/", UriKind.Absolute));

            GenerateTimeSlots();
            GenerateSpeakers();
            GenerateTracks();
            GenerateRooms();
            GenerateSessions();
        }

        private static void GenerateTimeSlots()
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            foreach (var time in _entities.timeslots)
            {
                DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(time.start, est);
                Console.WriteLine(String.Format("_timeById[\"{1}\"] = conference.GetTime(DateTime.Parse(\"{0}\"));", localStart, time.timeslotid));
            }
        }

        private static void GenerateSpeakers()
        {
            foreach (var speaker in _entities.speakers)
            {
                string speakerName = speaker.firstname + " " + speaker.lastname;
                string contact = "@" + speaker.twitter;
                string speakerBio = StringEncode(speaker.bio);
                string speakerImageUrl = speaker.imageurl;

                Console.WriteLine(String.Format("_speakerById[\"{4}\"] = conference.NewSpeaker(\"{0}\", \"{1}\", \"{2}\", \"{3}\");", speakerName, contact, speakerBio, speakerImageUrl, speaker.speakerid));
            }
        }

        private static void GenerateTracks()
        {
            foreach (var track in _entities.tracks)
            {
                Console.WriteLine(String.Format("_trackById[\"{0}\"] = conference.NewTrack(\"{1}\");", track.trackid, track.name));
            }
        }

        private static void GenerateRooms()
        {
            foreach (var room in _entities.rooms)
            {
                Console.WriteLine(String.Format("_roomById[\"{0}\"] = conference.NewRoom(\"{1}\");", room.roomid, room.name));
            }
        }

        private static void GenerateSessions()
        {
            foreach (var session in _entities.sessions)
            {
                string title = StringEncode(session.title);
                string description = StringEncode(session.description);
                Console.WriteLine(String.Format("conference.NewSessionPlace(\"{0}\", \"{1}\", \"{2}\", _speakerById[\"{3}\"], _trackById[\"{4}\"], _timeById[\"{5}\"], _roomById[\"{6}\"]);", session.sessionid, title, description, session.speakerid, session.trackid, session.timeslotid, session.roomid));
            }
        }

        private static string StringEncode(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\n", "\\n")
                .Replace("\"", "\\\"");
        }
    }
}
