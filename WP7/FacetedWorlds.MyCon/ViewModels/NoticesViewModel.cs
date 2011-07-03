using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using System.Linq;
using FacetedWorlds.MyCon.Models;
using System.Collections.Generic;
using System;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class NoticesViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly Clock _clock;

        public NoticesViewModel(Attendee attendee, ImageCache imageCache, Clock clock)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _clock = clock;
        }

        public IEnumerable<NoticeViewModel> Notices
        {
            get
            {
                var sessionNotices =
                    from schedule in _attendee.CurrentSchedules
                    from notice in schedule.SessionPlace.Session.Notices
                    select new SessionNoticeViewModel(notice, _imageCache, _clock) as NoticeViewModel;
                var conferenceNotices =
                    from notice in _attendee.Conference.Notices
                    select new ConferenceNoticeViewModel(notice, _clock) as NoticeViewModel;
                return sessionNotices.Union(conferenceNotices);
            }
        }

        public bool IsEmpty
        {
            get
            {
                var sessionNotices =
                    from schedule in _attendee.CurrentSchedules
                    from notice in schedule.SessionPlace.Session.Notices
                    select notice;
                var conferenceNotices =
                    from notice in _attendee.Conference.Notices
                    select notice;
                return !sessionNotices.Any() && !conferenceNotices.Any();
            }
        }
    }
}
