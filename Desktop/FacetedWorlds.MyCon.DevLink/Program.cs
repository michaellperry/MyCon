using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.DevLink.DevLinkServiceReference;
using System.Threading;

namespace FacetedWorlds.MyCon.DevLink
{
    class Program
    {
        private static devlink2011Entities _entities;

        static void Main(string[] args)
        {
            _entities = new devlink2011Entities(new Uri("http://devlink.us/odata.svc/", UriKind.Absolute));

            GenerateTimeSlots();
        }

        private static void GenerateTimeSlots()
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var times = _entities.timeslots;

            foreach (var time in times)
            {
                DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(time.start, est);
                Console.WriteLine(String.Format("_timeById[\"{1}\"] = conference.GetTime(DateTime.Parse(\"{0}\"));", localStart, time.timeslotid));
                if (time.description != "Morning Sessions" && time.description != "Afternoon Sessions")
                {
                    Console.WriteLine(String.Format("conference.NewGeneralSessionPlace(\"gen_{0}\", \"{1}\", _timeById[\"{2}\"], string.Empty);", time.timeslotid, time.description, time.timeslotid));
                }
            }
        }
    }
}
