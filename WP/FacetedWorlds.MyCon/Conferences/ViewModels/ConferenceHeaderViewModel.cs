using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceHeaderViewModel
    {
        private readonly ConferenceHeader _conferenceHeader;

        public ConferenceHeaderViewModel(ConferenceHeader conferenceHeader)
        {
            _conferenceHeader = conferenceHeader;
        }

        public string Name
        {
            get { return _conferenceHeader.Name; }
        }

        public string ImageUrl
        {
            get { return _conferenceHeader.ImageUrl; }
        }

        public DateTime Date
        {
            get { return _conferenceHeader.StartDate; }
        }

        public string Location
        {
            get { return _conferenceHeader.Location; }
        }
    }
}
