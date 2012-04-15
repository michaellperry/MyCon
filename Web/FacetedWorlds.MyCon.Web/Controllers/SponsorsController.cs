using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.ViewModels;

namespace FacetedWorlds.MyCon.Web.Controllers
{
    public class SponsorsController : Controller
    {
        //
        // GET: /Sponsors/

        public ActionResult Index()
        {
            return View(new SponsorsViewModel(MvcApplication.SynchronizationService.Conference));
        }
    }
}
