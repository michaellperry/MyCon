using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Model;
using System.IO;
using System.Reflection;

namespace FacetedWorlds.MyCon.DallasTechFest
{
    public static class XmlExtensions
    {
        public static XElement _(this XElement element, string name)
        {
            return element.Descendants(name).Single();
        }
    }
    public class DataLoader
    {
        public static void Load(Conference conference)
        {
            try
            {
                LoadTimes(conference);
                LoadSpeakers(conference);
                LoadSessions(conference);
                CreateLunchTimes(conference);
                AssignSessionPlaces(conference);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private static void LoadTimes(Conference conference)
        {
            string conferenceName = "Dallas TechFest 2011";
            if (conference.Name.Value != conferenceName)
                conference.Name = conferenceName;

            string conferenceMap = "http://img.docstoccdn.com/thumb/orig/10507230.png";
            if (conference.MapUrl != conferenceMap)
                conference.MapUrl = conferenceMap;

            conference.GetTime(new DateTime(2011, 8, 12, 9, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 12, 10, 30, 0));
            conference.GetTime(new DateTime(2011, 8, 12, 12, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 12, 13, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 12, 14, 30, 0));
            conference.GetTime(new DateTime(2011, 8, 12, 16, 0, 0));

            conference.GetTime(new DateTime(2011, 8, 13, 9, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 13, 10, 30, 0));
            conference.GetTime(new DateTime(2011, 8, 13, 12, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 13, 13, 0, 0));
            conference.GetTime(new DateTime(2011, 8, 13, 14, 30, 0));
            conference.GetTime(new DateTime(2011, 8, 13, 16, 0, 0));
        }

        private static void LoadSpeakers(Conference conference)
        {
            using (Stream speakerStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLoader), "speakers.xml"))
            {
                XDocument document = XDocument.Load(speakerStream);
                var posts =
                    from post in document.Descendants("div")
                    where post.Attribute("class").Value == "post"
                    select post;

                foreach (var post in posts)
                {
                    string speakerName = post._("h2")._("a").Value;
                    string correctedSpeakerName = CorrectSpeakerName(speakerName);
                    if (correctedSpeakerName != null)
                    {
                        var tds = post._("table")._("tr").Elements().ToArray();
                        string image = tds[0]._("img").Attribute("src").Value;
                        string[] paragraphs = tds[1].Elements().Select(p => p.Value).ToArray();

                        conference.NewSpeaker(correctedSpeakerName, null, String.Join("\n", paragraphs), image);
                    }
                }
            }
        }

        private static void LoadSessions(Conference conference)
        {
            using (Stream sessionsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLoader), "sessions.xml"))
            {
                XDocument document = XDocument.Load(sessionsStream);
                var posts = document.Descendants("div").Where(AttributeEquals("class", "post"));

                foreach (var post in posts)
                {
                    var anchors = post.Descendants("a").ToArray();
                    string speakerName = anchors[1].Value;
                    string correctedSpeakerName = CorrectSpeakerName(speakerName);
                    if (correctedSpeakerName != null)
                    {
                        string sessionId = anchors[0].Attribute("href").Value.Split('/')[2];
                        string sessionName = anchors[0].Value;
                        var entry = post.Descendants("div").Where(AttributeEquals("class", "entry")).Single();
                        var fields = entry.Descendants("div").Select(d => d.Value).ToList();
                        var category = fields.Where(f => f.StartsWith("Category: ")).Single().Substring("Category: ".Length);
                        var level = fields.Where(f => f.StartsWith("Level: ")).Single().Substring("Level: ".Length);
                        string[] paragraphs = entry.Elements("p").Select(p => p.Value).ToArray();

                        conference.NewSession(sessionId, sessionName, category, conference.GetSpeaker(correctedSpeakerName), level, String.Join("\n", paragraphs));
                    }
                }
            }
        }

        private static string CorrectSpeakerName(string speakerName)
        {
            if (speakerName == "Devlin")
                return null;
            if (speakerName == "Latish")
                return "Latish Sehgal";
            if (speakerName == "rob vettor")
                return "Rob Vettor";
            return speakerName;
        }

        private static Func<XElement, bool> AttributeEquals(string name, string value)
        {
            return delegate(XElement element)
            {
                XAttribute attribute = element.Attribute(name);
                if (attribute == null)
                    return false;
                return attribute.Value == value;
            };
        }

        private static void CreateLunchTimes(Conference conference)
        {
            var times = conference.Days
                .SelectMany(day => day.Times)
                .Where(time => time.Start.Hour == 12);

            foreach (Time time in times)
            {
                conference.NewGeneralSessionPlace(String.Format("Lunch_{0:yyyyMMdd}", time.Day.ConferenceDate), "Lunch", time, "Dining Area");
            }
        }

        private static void AssignSessionPlaces(Conference conference)
        {
            Time[] times = conference.Days
                .SelectMany(day => day.Times)
                .Where(time => time.Start.Hour != 12)
                .ToArray();
            Session[] sessions = conference.Sessions
                .Where(s => s.Track != null)
                .ToArray();

            int room = 0;
            int sessionIndex = 0;
            while (sessionIndex < sessions.Length)
            {
                for (int timeIndex = 0; timeIndex < times.Length && sessionIndex < sessions.Length; ++timeIndex, ++sessionIndex)
                {
                    Time time = times[timeIndex];
                    Session session = sessions[sessionIndex];

                    session.SetSessionPlace(time, (room + 101).ToString());
                }
                room++;
            }
        }
    }
}
