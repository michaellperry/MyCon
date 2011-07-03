using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using System;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ConferenceNoticeViewModel : NoticeViewModel
    {
        private readonly ConferenceNotice _notice;
        private readonly Clock _clock;

        public ConferenceNoticeViewModel(ConferenceNotice notice, Clock clock)
        {
            _notice = notice;
            _clock = clock;
        }

        public override string ImageUrl
        {
            get { return "/Images/logo.small.png"; }
        }

        public override string Title
        {
            get { return _notice.Conference.Name; }
        }

        public override string Text
        {
            get { return _notice.Text; }
        }

        public override string Age
        {
            get { return ToAgeText((int)((_clock.Time - _notice.TimeSent).TotalMinutes)); }
        }
    }
}
