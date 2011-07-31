using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Net;
using System.Diagnostics;
using HtmlAgilityPack;

namespace FacetedWorlds.MyCon.Capture
{
    class DataLoader
    {
        private Dictionary<string, Time> _timeById = new Dictionary<string, Time>();
        private Dictionary<string, Speaker> _speakerById = new Dictionary<string, Speaker>();
        private Dictionary<string, Track> _trackById = new Dictionary<string, Track>();
        private Dictionary<string, Room> _roomById = new Dictionary<string, Room>();

        public void Load(Conference conference)
        {
            LoadSpeakers(conference);
            //LoadSessions(conference);
            //CreateLunchTimes(conference);
            //AssignSessionPlaces(conference);
        }

        private void LoadSpeakers(Conference conference)
        {
            HtmlDocument speakersDocument = new HtmlDocument();
            using (Stream speakerStream = WebRequest.Create("http://dallastechfest.com/Speakers").GetResponse().GetResponseStream())
            {
                speakersDocument.Load(speakerStream);
                var posts = speakersDocument.DocumentNode.SelectNodes("//div[@class='post']");

                foreach (var post in posts)
                {
                    string speakerName = post.SelectSingleNode("h2/a").InnerText;
                    string correctedSpeakerName = CorrectSpeakerName(speakerName);
                    if (correctedSpeakerName != null)
                    {
                        var tds = post.SelectNodes(".//td");
                        string image = tds[0].SelectSingleNode(".//img").GetAttributeValue("src", string.Empty);
                        if (!image.StartsWith("http:"))
                            image = null;
                        string[] paragraphs = tds[1].SelectNodes("p").Select(p => p.InnerText).ToArray();

                        conference.NewSpeaker(correctedSpeakerName, null, String.Join("\n", paragraphs), image);
                    }
                }
            }
        }

        private void LoadSessions(Conference conference)
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

        private string CorrectSpeakerName(string speakerName)
        {
            if (speakerName == "Devlin")
                return null;
            if (speakerName == "Latish")
                return "Latish Sehgal";
            if (speakerName == "rob vettor")
                return "Rob Vettor";
            return speakerName;
        }

        private Func<XElement, bool> AttributeEquals(string name, string value)
        {
            return delegate(XElement element)
            {
                XAttribute attribute = element.Attribute(name);
                if (attribute == null)
                    return false;
                return attribute.Value == value;
            };
        }

        private void CreateLunchTimes(Conference conference)
        {
            Time[] times = conference.Days
                .SelectMany(day => day.Times)
                .Where(time => time.Start.Hour == 12)
                .ToArray();

            foreach (Time time in times)
            {
                conference.NewGeneralSessionPlace(String.Format("Lunch_{0:yyyyMMdd}", time.Day.ConferenceDate), "Lunch", time, "Dining Area");
            }
        }

        private void AssignSessionPlaces(Conference conference)
        {
            Time[] times = conference.Days
                .SelectMany(day => day.Times)
                .Where(time => time.Start.Hour != 12)
                .ToArray();
            Session[] sessions = conference.Sessions.ToArray();

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
