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
        private readonly Individual _individual;
        private readonly ConferenceSelection _conferenceSelection;

        public ConferenceDetailsViewModel(
            Individual individual,
            ConferenceHeader conferenceHeader,
            ConferenceSelection conferenceSelection)
            : base(conferenceHeader)
        {
            _individual = individual;
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

        public Guid JoinConference()
        {
            Profile profile = _individual.EnsureProfile();
            Attendee attendee = profile.Attending(ConferenceHeader.Conference);
            foreach (var inactive in attendee.Inactives.Ensure())
                inactive.MakeActive();
            return ConferenceHeader.Conference.Unique;
        }
    }
}
