using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Web.ViewModels;

namespace FacetedWorlds.MyCon.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeViewModel(MvcApplication.SynchronizationService.Conference));
        }

    }
}
