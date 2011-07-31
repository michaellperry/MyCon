using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Model;
using HtmlAgilityPack;

namespace FacetedWorlds.MyCon.Capture
{
    class DataLoader
    {
        private Dictionary<string, Time> _timeById = new Dictionary<string, Time>();
        private Dictionary<string, Speaker> _speakerById = new Dictionary<string, Speaker>();
        private Dictionary<string, Track> _trackById = new Dictionary<string, Track>();
        private Dictionary<string, Room> _roomById = new Dictionary<string, Room>();
        private Dictionary<string, Session> _sessionById = new Dictionary<string,Session>();

        public void Load(Conference conference)
        {
            LoadSpeakers(conference);
            LoadSessions(conference);
            LoadSessionTimes(conference);
            CreateLunchTimes(conference);
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
            HtmlDocument sessionsDocument = new HtmlDocument();
            using (Stream sessionsStream = WebRequest.Create("http://dallastechfest.com/Sessions?tab=list").GetResponse().GetResponseStream())
            {
                sessionsDocument.Load(sessionsStream);
                var posts = sessionsDocument.DocumentNode.SelectNodes("//div[@class='post']");

                foreach (var post in posts)
                {
                    var anchors = post.SelectNodes(".//a").ToArray();
                    string speakerName = anchors[1].InnerText;
                    string correctedSpeakerName = CorrectSpeakerName(speakerName);
                    if (correctedSpeakerName != null)
                    {
                        string sessionId = anchors[0].GetAttributeValue("href", string.Empty).Split('/')[2];
                        string sessionName = HttpUtility.HtmlDecode(anchors[0].InnerText);
                        var entry = post.SelectSingleNode(".//div[@class='entry']");
                        var fields = entry.SelectNodes(".//div").Select(d => d.InnerText).ToList();
                        var category = fields.Where(f => f.StartsWith("Category: ")).Single().Substring("Category: ".Length);
                        var level = fields.Where(f => f.StartsWith("Level: ")).Single().Substring("Level: ".Length);
                        string[] paragraphs = entry.SelectNodes("p").Select(p => HttpUtility.HtmlDecode(p.InnerText)).ToArray();

                        _sessionById[sessionId] = conference.NewSession(sessionId, sessionName, category, conference.GetSpeaker(correctedSpeakerName), level, String.Join("\n", paragraphs));
                    }
                }
            }
        }

        private void LoadSessionTimes(Conference conference)
        {
            HtmlDocument sessionsDocument = new HtmlDocument();
            using (Stream sessionsStream = WebRequest.Create("http://dallastechfest.com/Sessions?tab=times").GetResponse().GetResponseStream())
            {
                sessionsDocument.Load(sessionsStream);
                var slots = sessionsDocument.DocumentNode.SelectNodes("//h3").Where(slot => slot.InnerText != "None");
                foreach (var slot in slots)
                {
                    DateTime startTime = DateTime.Parse("8/12/2011 " + slot.InnerText.Substring(0, 8));
                    var sessions = slot.NextSibling.NextSibling.SelectNodes(".//tr[@class='status_Accepted']");
                    foreach (var session in sessions)
                    {
                        var anchors = session.SelectNodes(".//a").ToArray();
                        string sessionId = anchors[1].GetAttributeValue("href", string.Empty).Split('/')[2];

                        conference.NewSessionPlace(_sessionById[sessionId], startTime, "TBD");
                    }
                }
            }
        }

        private string CorrectSpeakerName(string speakerName)
        {
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
            conference.NewGeneralSessionPlace("lunch_08122011", "Lunch", conference.GetTime(DateTime.Parse("8/12/2011 11:45 AM")), "TBD", "http://qedcode.com/images/lunch.png");
            conference.NewGeneralSessionPlace("lunch_08132011", "Lunch", conference.GetTime(DateTime.Parse("8/13/2011 11:45 AM")), "TBD", "http://qedcode.com/images/lunch.png");
        }
    }
}
