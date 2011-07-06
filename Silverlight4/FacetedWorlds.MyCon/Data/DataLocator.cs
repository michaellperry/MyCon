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
                            string image = tds[0].Descendants("img").Single().Attribute("src").Value;
                            string[] paragraphs = tds[1].Elements().Select(p => p.Value).ToArray();

                            conference.NewSpeaker(correctedSpeakerName, null, String.Join("\n", paragraphs), image);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
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
    }
}
