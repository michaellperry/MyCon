using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class HomeViewModel
    {
        private Conference _conference;

        public HomeViewModel(Conference conference)
        {
            _conference = conference;
        }

        public string ConferenceName
        {
            get { return _conference.Name; }
        }
    }
}