using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.ViewModels;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.Controllers
{
    public class TracksController : Controller
    {
        //
        // GET: /Tracks/

        public ActionResult Index()
        {
            return View(new TracksViewModel(MvcApplication.SynchronizationService.Conference));
        }

        //
        // GET: /Tracks/Details/Web

        public ActionResult Details(string id)
        {
            Track track = MvcApplication.SynchronizationService.Conference.FindTrack(id);
            if (track == null)
                return HttpNotFound();

            return View(new TrackDetailsViewModel(track));
        }
    }
}
