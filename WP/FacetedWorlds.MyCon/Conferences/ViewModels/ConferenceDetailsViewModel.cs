using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Conferences.Models;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceDetailsViewModel : ConferenceHeaderViewModelBase
    {
        private readonly ConferenceSelection _conferenceSelection;

        public ConferenceDetailsViewModel(
            ConferenceHeader conferenceHeader,
            ConferenceSelection conferenceSelection)
            : base(conferenceHeader)
        {
            _conferenceSelection = conferenceSelection;
        }

        public string Address
        {
            get { return ConferenceHeader.Address; }
        }

        public string HomePageUrl
        {
            get { return ConferenceHeader.HomePageUrl; }
        }

        public string Description
        {
            get { return ConferenceHeader.Description.Value.GetString(); }
        }

        public void NavigatedFrom()
        {
            _conferenceSelection.SelectedConference = null;
        }
    }
}
