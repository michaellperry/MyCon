using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.DevLink
{
    public class DataLoader
    {
        public static void LoadTimes(Conference conference)
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
                        DateTime start = DateTime.Parse(startString);
                        string endString = entry.Element(XName.Get("end", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;
                        DateTime end = DateTime.Parse(endString);
                        string description = entry.Element(XName.Get("description", "http://schemas.microsoft.com/ado/2007/08/dataservices")).Value;

                        Time time = conference.GetTime(start);

                        if (description != "Morning Sessions" && description != "Afternoon Sessions")
                        {
                            // General session.
                            conference.NewGeneralSessionPlace(timeslotId, description, time, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
