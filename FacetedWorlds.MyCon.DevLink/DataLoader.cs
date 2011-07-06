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
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.DevLink.DevLinkServiceReference;
using System.Linq;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.DevLink
{
    public class DataLoader
    {
        public static void LoadTimes(Conference conference)
        {
            try
            {
                devlink2011Entities entities = new devlink2011Entities(new Uri("http://devlink.us/odata.svc/", UriKind.Absolute));

                entities.timeslots.BeginExecute(new AsyncCallback(delegate(IAsyncResult result)
                {
                    try
                    {
                        IEnumerable<timeslot> timeSlots = entities.timeslots.EndExecute(result);
                        List<timeslot> list = timeSlots.ToList();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
