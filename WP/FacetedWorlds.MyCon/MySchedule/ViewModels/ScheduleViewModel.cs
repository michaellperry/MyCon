using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using Microsoft.Phone.Shell;

namespace FacetedWorlds.MyCon.MySchedule.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Attendee _attendee;

        public ScheduleViewModel(Attendee attendee)
        {
            _attendee = attendee;
        }

        public ShellTileData GetTileData()
        {
            var conferenceHeader = _attendee.Conference.ConferenceHeaders.Ensure()
                .FirstOrDefault();
            if (conferenceHeader == null)
                return null;

            Uri imageUri = new Uri(conferenceHeader.ImageUrl.Ensure(), UriKind.Absolute);
            return new StandardTileData()
            {
               Title = conferenceHeader.Name.Ensure(),
               BackTitle = conferenceHeader.Name.Ensure(),
               BackContent = "Your next session...",
               Count = 0,
               BackgroundImage = imageUri,
               BackBackgroundImage = null
            };
        }

        public void LeaveConference()
        {
            _attendee.MakeInactive();
        }
    }
}
