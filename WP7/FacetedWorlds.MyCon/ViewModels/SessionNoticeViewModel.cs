using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using System;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionNoticeViewModel : NoticeViewModel
    {
        private readonly SessionNotice _notice;
        private readonly ImageCache _imageCache;
        private readonly Clock _clock;

        public SessionNoticeViewModel(SessionNotice notice, ImageCache imageCache, Clock clock)
        {
            _notice = notice;
            _imageCache = imageCache;
            _clock = clock;
        }

        public override string ImageUrl
        {
            get { return _imageCache.SmallImageUrl(_notice.Session.Speaker.ImageUrl); }
        }

        public override string Title
        {
            get { return _notice.Session.Name; }
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
