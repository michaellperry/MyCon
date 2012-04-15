using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.ViewModels;

namespace FacetedWorlds.MyCon.Web.Controllers
{
    public class SpeakersController : Controller
    {
        //
        // GET: /Speakers/

        public ActionResult Index()
        {
            return View(new SpeakersViewModel(MvcApplication.SynchronizationService.Conference));
        }

        //
        // GET: /Speakers/Details/Amir%20Rajan

        public ActionResult Details(string id)
        {
            var speaker = MvcApplication.SynchronizationService.Conference.FindSpeaker(id);
            if (speaker == null)
                return HttpNotFound();

            return View(new SpeakerDetailsViewModel(speaker));
        }
    }
}
