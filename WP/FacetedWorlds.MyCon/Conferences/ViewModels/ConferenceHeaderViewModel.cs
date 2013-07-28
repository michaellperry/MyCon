using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceHeaderViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
