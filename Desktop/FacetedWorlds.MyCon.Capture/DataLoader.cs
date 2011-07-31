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
        private Conference _conference;
        private Dictionary<string, Time> _timeById = new Dictionary<string, Time>();
        private Dictionary<string, Speaker> _speakerById = new Dictionary<string, Speaker>();
        private Dictionary<string, Track> _trackById = new Dictionary<string, Track>();
        private Dictionary<string, Room> _roomById = new Dictionary<string, Room>();
        private Dictionary<string, Session> _sessionById = new Dictionary<string,Session>();
        
        public void Load(Conference conference)
        {
            _conference = conference;
            SetConferenceName("Dallas TechFest 2011");
            SetConferenceMap("http://img.docstoccdn.com/thumb/orig/10507230.png");
            LoadSpeakers();
            LoadSessions();
            //LoadSessionTimes();
            SetSessionTimes();
            CreateLunchTimes();
        }

        private void SetConferenceName(string name)
        {
            if (_conference.Name.Value != name)
                _conference.Name = name;
        }

        private void SetConferenceMap(string mapUrl)
        {
            if (_conference.MapUrl.Value != mapUrl)
                _conference.MapUrl = mapUrl;
        }

        private void LoadSpeakers()
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

                        _conference.NewSpeaker(correctedSpeakerName, null, String.Join("\n", paragraphs), image);
                    }
                }
            }
        }

        private void LoadSessions()
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

                        _sessionById[sessionId] = _conference.NewSession(sessionId, sessionName, category, _conference.GetSpeaker(correctedSpeakerName), level, String.Join("\n", paragraphs));
                    }
                }
            }
        }

        private void LoadSessionTimes()
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

                        SetTimeOfSession(startTime, sessionId);
                    }
                }
            }
        }

        private void SetSessionTimes()
        {
            DateTime d1s1 = new DateTime(2011, 8, 12, 9, 0, 0);
            SetTimeOfSession(d1s1, "720");
            SetTimeOfSession(d1s1, "717");
            SetTimeOfSession(d1s1, "647");
            SetTimeOfSession(d1s1, "722");
            SetTimeOfSession(d1s1, "873");
            SetTimeOfSession(d1s1, "811");
            SetTimeOfSession(d1s1, "95");
            SetTimeOfSession(d1s1, "1210");
            SetTimeOfSession(d1s1, "1212");

            DateTime d1s2 = new DateTime(2011, 8, 12, 10, 30, 0);
            SetTimeOfSession(d1s2, "720");
            SetTimeOfSession(d1s2, "1372");
            SetTimeOfSession(d1s2, "712");
            SetTimeOfSession(d1s2, "647");
            SetTimeOfSession(d1s2, "722");
            SetTimeOfSession(d1s2, "886");
            SetTimeOfSession(d1s2, "796");
            SetTimeOfSession(d1s2, "787");
            SetTimeOfSession(d1s2, "1007");
            SetTimeOfSession(d1s2, "393");

            DateTime d1s3 = new DateTime(2011, 8, 12, 12, 45, 0);
            SetTimeOfSession(d1s3, "721");
            SetTimeOfSession(d1s3, "1373");
            SetTimeOfSession(d1s3, "800");
            SetTimeOfSession(d1s3, "790");
            SetTimeOfSession(d1s3, "619");
            SetTimeOfSession(d1s3, "752");
            SetTimeOfSession(d1s3, "1295");
            SetTimeOfSession(d1s3, "99");
            SetTimeOfSession(d1s3, "544");
            SetTimeOfSession(d1s3, "1207");

            DateTime d1s4 = new DateTime(2011, 8, 12, 14, 15, 0);
            SetTimeOfSession(d1s4, "83");
            SetTimeOfSession(d1s4, "646");
            SetTimeOfSession(d1s4, "392");
            SetTimeOfSession(d1s4, "716");
            SetTimeOfSession(d1s4, "977");
            SetTimeOfSession(d1s4, "663");
            SetTimeOfSession(d1s4, "107");
            SetTimeOfSession(d1s4, "545");
            SetTimeOfSession(d1s4, "183");

            DateTime d1s5 = new DateTime(2011, 8, 12, 15, 45, 0);
            SetTimeOfSession(d1s5, "889");
            SetTimeOfSession(d1s5, "1358");
            SetTimeOfSession(d1s5, "902");
            SetTimeOfSession(d1s5, "715");
            SetTimeOfSession(d1s5, "737");
            SetTimeOfSession(d1s5, "799");
            SetTimeOfSession(d1s5, "91");
            SetTimeOfSession(d1s5, "829");
            SetTimeOfSession(d1s5, "466");
            SetTimeOfSession(d1s5, "140");

            DateTime d2s1 = new DateTime(2011, 8, 13, 9, 0, 0);
            SetTimeOfSession(d2s1, "115");
            SetTimeOfSession(d2s1, "1211");
            SetTimeOfSession(d2s1, "104");
            SetTimeOfSession(d2s1, "722");
            SetTimeOfSession(d2s1, "713");
            SetTimeOfSession(d2s1, "71");
            SetTimeOfSession(d2s1, "631");
            SetTimeOfSession(d2s1, "782");
            SetTimeOfSession(d2s1, "215");
            SetTimeOfSession(d2s1, "1206");

            DateTime d2s2 = new DateTime(2011, 8, 13, 10, 30, 0);
            SetTimeOfSession(d2s2, "113");
            SetTimeOfSession(d2s2, "1371");
            SetTimeOfSession(d2s2, "1369");
            SetTimeOfSession(d2s2, "722");
            SetTimeOfSession(d2s2, "302");
            SetTimeOfSession(d2s2, "196");
            SetTimeOfSession(d2s2, "96");
            SetTimeOfSession(d2s2, "496");
            SetTimeOfSession(d2s2, "123");

            DateTime d2s3 = new DateTime(2011, 8, 13, 12, 45, 0);
            SetTimeOfSession(d2s3, "1202");
            SetTimeOfSession(d2s3, "1353");
            SetTimeOfSession(d2s3, "110");
            SetTimeOfSession(d2s3, "797");
            SetTimeOfSession(d2s3, "947");
            SetTimeOfSession(d2s3, "105");
            SetTimeOfSession(d2s3, "666");
            SetTimeOfSession(d2s3, "196");
            SetTimeOfSession(d2s3, "89");
            SetTimeOfSession(d2s3, "1209");

            DateTime d2s4 = new DateTime(2011, 8, 13, 14, 15, 0);
            SetTimeOfSession(d2s4, "903");
            SetTimeOfSession(d2s4, "1354");
            SetTimeOfSession(d2s4, "1370");
            SetTimeOfSession(d2s4, "481");
            SetTimeOfSession(d2s4, "617");
            SetTimeOfSession(d2s4, "376");
            SetTimeOfSession(d2s4, "124");
            SetTimeOfSession(d2s4, "783");
            SetTimeOfSession(d2s4, "214");
            SetTimeOfSession(d2s4, "1293");

            DateTime d2s5 = new DateTime(2011, 8, 13, 15, 45, 0);
            SetTimeOfSession(d2s5, "1355");
            SetTimeOfSession(d2s5, "766");
            SetTimeOfSession(d2s5, "718");
            SetTimeOfSession(d2s5, "64");
            SetTimeOfSession(d2s5, "74");
            SetTimeOfSession(d2s5, "1131");
            SetTimeOfSession(d2s5, "287");
            SetTimeOfSession(d2s5, "1208");
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

        private void CreateLunchTimes()
        {
            _conference.NewGeneralSessionPlace("lunch_08122011", "Lunch", _conference.GetTime(DateTime.Parse("8/12/2011 11:45 AM")), "TBD", "http://qedcode.com/images/lunch.png");
            _conference.NewGeneralSessionPlace("lunch_08132011", "Lunch", _conference.GetTime(DateTime.Parse("8/13/2011 11:45 AM")), "TBD", "http://qedcode.com/images/lunch.png");
        }

        private void SetTimeOfSession(DateTime startTime, string sessionId)
        {
            Session session;
            if (_sessionById.TryGetValue(sessionId, out session))
                _conference.NewSessionPlace(session, startTime, "TBD");
        }
    }
}
