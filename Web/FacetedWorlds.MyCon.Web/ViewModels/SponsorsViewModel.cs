using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SponsorsViewModel
    {
        private readonly Conference _conference;

        public SponsorsViewModel(Conference conference)
        {
            _conference = conference;
        }

        public string ConferenceName
        {
            get { return _conference.Name; }
        }
    }
}