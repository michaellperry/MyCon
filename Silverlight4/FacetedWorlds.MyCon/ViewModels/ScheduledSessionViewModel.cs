using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ScheduledSessionViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public ScheduledSessionViewModel(SessionPlace sessionPlace)
        {
            _sessionPlace = sessionPlace;
        }
    }
}
