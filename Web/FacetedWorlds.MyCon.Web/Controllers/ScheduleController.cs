using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.ViewModels;
using System.Text.RegularExpressions;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.Controllers
{
    public class ScheduleController : Controller
    {
        //
        // GET: /Schedule/

        public ActionResult Index()
        {
            return View(new ScheduleViewModel(MvcApplication.SynchronizationService.Conference));
        }

        //
        // GET: /Schedule/Details/5

        public ActionResult Details(string id)
        {
            Regex dateTime = new Regex(@"(?<year>\d\d\d\d)(?<month>\d\d)(?<day>\d\d)(?<hour>\d\d)(?<minute>\d\d)");
            Match match = dateTime.Match(id);
            if (!match.Success)
                return HttpNotFound();

            DateTime start = new DateTime(
                int.Parse(match.Groups["year"].Value),
                int.Parse(match.Groups["month"].Value),
                int.Parse(match.Groups["day"].Value),
                int.Parse(match.Groups["hour"].Value),
                int.Parse(match.Groups["minute"].Value),
                0);
            Time time = MvcApplication.SynchronizationService.Conference.FindTime(start);
            if (time == null)
                return HttpNotFound();

            return View(new TimeDetailsViewModel(time));
        }
    }
}
