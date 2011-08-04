using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Conference _conference;

        public ScheduleViewModel(Conference conference)
        {
            _conference = conference;
        }
    }
}
