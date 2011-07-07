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
        public static void GenerateCode()
        {
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataLoader), "TimeSlots.xml"))
                {
                    XDocument responseDocument = XDocument.Load(stream);
                    List<XElement> entries = responseDocument.Descendants(XName.Get("properties", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")).ToList();
                    foreach (XElement entry in entries)
                    {
                        string timeslotId = entry.Element(XName.Get("timeslotid", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;
                        string startString = entry.Element(XName.Get("start", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;
                        string endString = entry.Element(XName.Get("end", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;
                        string description = entry.Element(XName.Get("description", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;

                        if (description != "Morning Sessions" && description != "Afternoon Sessions")
                        {
                            Debug.WriteLine(String.Format("conference.NewGeneralSessionPlace(\"{1}\", \"{2}\", conference.GetTime(DateTime.Parse(\"{0}\")), string.Empty);", startString, timeslotId, description));
                        }
                        else
                        {
                            Debug.WriteLine(String.Format("conference.GetTime(DateTime.Parse(\"{0}\"));", startString));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void LoadData(Conference conference)
        {
            conference.NewGeneralSessionPlace("1", "Registration / Exhibit Hall", conference.GetTime(DateTime.Parse("2011-08-17T11:30:00Z")), string.Empty);
            conference.NewGeneralSessionPlace("2", "Opening Keynote", conference.GetTime(DateTime.Parse("2011-08-17T12:30:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-17T14:15:00Z"));
            conference.NewGeneralSessionPlace("4", "Lunch", conference.GetTime(DateTime.Parse("2011-08-17T15:30:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-17T17:00:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-17T18:30:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-17T20:00:00Z"));
            conference.NewGeneralSessionPlace("8", "Networking Event", conference.GetTime(DateTime.Parse("2011-08-17T23:00:00Z")), string.Empty);
            conference.NewGeneralSessionPlace("9", "Registration / Exhibit Hall", conference.GetTime(DateTime.Parse("2011-08-18T12:00:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-18T12:00:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-18T13:30:00Z"));
            conference.NewGeneralSessionPlace("12", "Lunch", conference.GetTime(DateTime.Parse("2011-08-18T15:00:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-18T17:00:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-18T18:30:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-18T20:00:00Z"));
            conference.NewGeneralSessionPlace("16", "Networking Event", conference.GetTime(DateTime.Parse("2011-08-18T22:45:00Z")), string.Empty);
            conference.NewGeneralSessionPlace("17", "Registration / Exhibit Hall", conference.GetTime(DateTime.Parse("2011-08-19T12:00:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-19T12:00:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-19T13:30:00Z"));
            conference.NewGeneralSessionPlace("20", "Lunch", conference.GetTime(DateTime.Parse("2011-08-19T15:00:00Z")), string.Empty);
            conference.GetTime(DateTime.Parse("2011-08-19T17:00:00Z"));
            conference.GetTime(DateTime.Parse("2011-08-19T18:30:00Z"));
            conference.NewGeneralSessionPlace("23", "Closing Keynote & Giveaways", conference.GetTime(DateTime.Parse("2011-08-19T20:00:00Z")), string.Empty);
        }
    }
}
