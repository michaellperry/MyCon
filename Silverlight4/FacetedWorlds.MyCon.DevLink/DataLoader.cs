using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Model;
using System.Diagnostics;

namespace FacetedWorlds.MyCon.DevLink
{
    public class DataLoader
    {
        private Dictionary<string, Time> _timeById = new Dictionary<string, Time>();

        public static void GenerateCode()
        {
            try
            {
                GenerateTimes();
                GenerateSessions();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static void GenerateTimes()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLoader), "TimeSlots.xml"))
            {
                XDocument responseDocument = XDocument.Load(stream);
                List<XElement> entries = responseDocument.Descendants(XName.Get("properties", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")).ToList();
                foreach (XElement entry in entries)
                {
                    string timeslotId = GetValue(entry, "timeslotid");
                    string startString = GetValue(entry, "start");
                    string endString = GetValue(entry, "end");
                    string description = GetValue(entry, "description");

                    Debug.WriteLine(String.Format("_timeById[\"{1}\"] = conference.GetTime(DateTime.Parse(\"{0}\"));", startString, timeslotId));
                    if (description != "Morning Sessions" && description != "Afternoon Sessions")
                    {
                        Debug.WriteLine(String.Format("conference.NewGeneralSessionPlace(\"gen_{1}\", \"{2}\", _timeById[\"{3}\"], string.Empty);", startString, timeslotId, description, timeslotId));
                    }
                }
            }
        }

        private static void GenerateSessions()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLoader), "Sessions.xml"))
            {
                XDocument responseDocument = XDocument.Load(stream);
                List<XElement> entries = responseDocument.Descendants(XName.Get("properties", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")).ToList();
                foreach (XElement entry in entries)
                {
                    string sessionid = GetValue(entry, "sessionid");
                    string trackid = GetValue(entry, "trackid");
                    string sessionlevelid = GetValue(entry, "sessionlevelid");
                    string title = GetValue(entry, "title");
                    string description = GetValue(entry, "description");
                    string roomid = GetValue(entry, "roomid");
                    string timeslotid = GetValue(entry, "timeslotid");
                    string speakerid = GetValue(entry, "speakerid");
                }
            }
        }

        public void LoadData(Conference conference)
        {
            LoadTimes(conference);
        }

        private void LoadTimes(Conference conference)
        {
            _timeById["1"] = conference.GetTime(DateTime.Parse("2011-08-17T11:30:00Z"));
            conference.NewGeneralSessionPlace("gen_1", "Registration / Exhibit Hall", _timeById["1"], string.Empty);
            _timeById["2"] = conference.GetTime(DateTime.Parse("2011-08-17T12:30:00Z"));
            conference.NewGeneralSessionPlace("gen_2", "Opening Keynote", _timeById["2"], string.Empty);
            _timeById["3"] = conference.GetTime(DateTime.Parse("2011-08-17T14:15:00Z"));
            _timeById["4"] = conference.GetTime(DateTime.Parse("2011-08-17T15:30:00Z"));
            conference.NewGeneralSessionPlace("gen_4", "Lunch", _timeById["4"], string.Empty);
            _timeById["5"] = conference.GetTime(DateTime.Parse("2011-08-17T17:00:00Z"));
            _timeById["6"] = conference.GetTime(DateTime.Parse("2011-08-17T18:30:00Z"));
            _timeById["7"] = conference.GetTime(DateTime.Parse("2011-08-17T20:00:00Z"));
            _timeById["8"] = conference.GetTime(DateTime.Parse("2011-08-17T23:00:00Z"));
            conference.NewGeneralSessionPlace("gen_8", "Networking Event", _timeById["8"], string.Empty);
            _timeById["9"] = conference.GetTime(DateTime.Parse("2011-08-18T12:00:00Z"));
            conference.NewGeneralSessionPlace("gen_9", "Registration / Exhibit Hall", _timeById["9"], string.Empty);
            _timeById["10"] = conference.GetTime(DateTime.Parse("2011-08-18T12:00:00Z"));
            _timeById["11"] = conference.GetTime(DateTime.Parse("2011-08-18T13:30:00Z"));
            _timeById["12"] = conference.GetTime(DateTime.Parse("2011-08-18T15:00:00Z"));
            conference.NewGeneralSessionPlace("gen_12", "Lunch", _timeById["12"], string.Empty);
            _timeById["13"] = conference.GetTime(DateTime.Parse("2011-08-18T17:00:00Z"));
            _timeById["14"] = conference.GetTime(DateTime.Parse("2011-08-18T18:30:00Z"));
            _timeById["15"] = conference.GetTime(DateTime.Parse("2011-08-18T20:00:00Z"));
            _timeById["16"] = conference.GetTime(DateTime.Parse("2011-08-18T22:45:00Z"));
            conference.NewGeneralSessionPlace("gen_16", "Networking Event", _timeById["16"], string.Empty);
            _timeById["17"] = conference.GetTime(DateTime.Parse("2011-08-19T12:00:00Z"));
            conference.NewGeneralSessionPlace("gen_17", "Registration / Exhibit Hall", _timeById["17"], string.Empty);
            _timeById["18"] = conference.GetTime(DateTime.Parse("2011-08-19T12:00:00Z"));
            _timeById["19"] = conference.GetTime(DateTime.Parse("2011-08-19T13:30:00Z"));
            _timeById["20"] = conference.GetTime(DateTime.Parse("2011-08-19T15:00:00Z"));
            conference.NewGeneralSessionPlace("gen_20", "Lunch", _timeById["20"], string.Empty);
            _timeById["21"] = conference.GetTime(DateTime.Parse("2011-08-19T17:00:00Z"));
            _timeById["22"] = conference.GetTime(DateTime.Parse("2011-08-19T18:30:00Z"));
            _timeById["23"] = conference.GetTime(DateTime.Parse("2011-08-19T20:00:00Z"));
            conference.NewGeneralSessionPlace("gen_23", "Closing Keynote & Giveaways", _timeById["23"], string.Empty);
        }

        private static string GetValue(XElement entry, string name)
        {
            return entry.Element(XName.Get(name, "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;
        }
    }
}
