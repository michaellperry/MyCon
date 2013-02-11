using System;
using FacetedWorlds.MyCon.Model;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class SessionHeaderViewModel
    {
        private readonly SessionPlace _sessionPlace;

        public SessionHeaderViewModel(SessionPlace sessionPlace)
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

        public string Subtitle
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null)
                    return null;

                return _sessionPlace.Session.Speaker.Name;
            }
        }

        public ImageSource Image
        {
            get
            {
                if (_sessionPlace.Session == null ||
                    _sessionPlace.Session.Speaker == null ||
                    _sessionPlace.Session.Speaker.ImageUrl.Value == null)
                    return null;

                return new BitmapImage(new Uri(_sessionPlace.Session.Speaker.ImageUrl.Value, UriKind.Absolute));
            }
        }
    }
}
