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
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Model;
using System.Linq;

namespace FacetedWorlds.MyCon.Data
{
    public static class XmlExtensions
    {
        public static XElement _(this XElement element, string name)
        {
            return element.Descendants(name).Single();
        }
    }
    public class DataLocator
    {
        public static void Load(Conference conference)
        {
            try
            {
                LoadSpeakers(conference);
                LoadSessions(conference);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private static void LoadSpeakers(Conference conference)
        {
            using (Stream speakerStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLocator), "speakers.xml"))
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
            using (Stream sessionsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLocator), "sessions.xml"))
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
    }
}
