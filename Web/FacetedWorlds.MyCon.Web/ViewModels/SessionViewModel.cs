using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Web.Extensions;

namespace FacetedWorlds.MyCon.Web.ViewModels
{
    public class SessionViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public SessionViewModel(SessionPlace sessionPlace)
        {
            _sessionPlace = sessionPlace;
        }

        public string Name
        {
            get
            {
                if (_sessionPlace.Session == null)
                    return null;

                return _sessionPlace.Session.Name;
            }
        }

        public string Speaker
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                return _sessionPlace.Session.Speaker.Name;
            }
        }

        public string SpeakerImageUrl
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                return _sessionPlace.Session.Speaker.ImageUrl;
            }
        }

        public string Day
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.PlaceTime == null)
                    return null;

                return string.Format("{0:ddd}", _sessionPlace.Place.PlaceTime.Start.ConvertTo("Central Standard Time"));
            }
        }

        public string Time
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.PlaceTime == null)
                    return null;

                return string.Format("{0:h:mm}", _sessionPlace.Place.PlaceTime.Start.ConvertTo("Central Standard Time"));
            }
        }

        public string TimeSlot
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.PlaceTime == null)
                    return null;

                return string.Format("{0:yyyyMMddHHmm}", _sessionPlace.Place.PlaceTime.Start.ConvertTo("Central Standard Time"));
            }
        }

        public string Track
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Track == null)
                    return null;

                return _sessionPlace.Session.Track.Name;
            }
        }

        public string Room
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.Room == null)
                    return null;

                return _sessionPlace.Place.Room.RoomNumber;
            }
        }

        public MvcHtmlString Description
        {
            get
            {
                if (_sessionPlace.Session == null)
                    return null;

                IEnumerable<DocumentSegment> segments = _sessionPlace.Session.Description.Value;
                if (segments == null)
                    return new MvcHtmlString(String.Empty);

                string raw = string.Join("", segments.Select(segment => segment.Text).ToArray());
                var lines = raw.Split('\r').Where(l => !String.IsNullOrWhiteSpace(l));
                var paragraphs = lines.Select(l => String.Format("<p>{0}</p>", HttpUtility.HtmlEncode(l)));
                var html = string.Join("", paragraphs.ToArray());
                return new MvcHtmlString(html);
            }
        }
    }
}
