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
        }

        private static void GenerateTimeSlots()
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            foreach (var time in _entities.timeslots)
            {
                DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(time.start, est);
                Console.WriteLine(String.Format("_timeById[\"{1}\"] = conference.GetTime(DateTime.Parse(\"{0}\"));", localStart, time.timeslotid));
                if (time.description != "Morning Sessions" && time.description != "Afternoon Sessions")
                {
                    Console.WriteLine(String.Format("conference.NewGeneralSessionPlace(\"gen_{0}\", \"{1}\", _timeById[\"{2}\"], string.Empty);", time.timeslotid, time.description, time.timeslotid));
                }
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

        private static string StringEncode(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\n", "\\n")
                .Replace("\"", "\\\"");
        }
    }
}
