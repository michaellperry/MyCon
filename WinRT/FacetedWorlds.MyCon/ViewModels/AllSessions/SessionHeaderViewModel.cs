using System;
using FacetedWorlds.MyCon.Model;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using FacetedWorlds.MyCon.Models;
using Windows.UI.Xaml;

namespace FacetedWorlds.MyCon.ViewModels.AllSessions
{
    public class SessionHeaderViewModel
    {
        private readonly SessionPlace _sessionPlace;
        private readonly SelectionModel _selectionModel;
        
        public SessionHeaderViewModel(SessionPlace sessionPlace, SelectionModel selectionModel)
        {
            _sessionPlace = sessionPlace;
            _selectionModel = selectionModel;
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
                    String.IsNullOrEmpty(_sessionPlace.Session.Speaker.ImageUrl.Value))
                    return null;

                return new BitmapImage(new Uri(_sessionPlace.Session.Speaker.ImageUrl.Value, UriKind.Absolute));
            }
        }

        public void Select()
        {
            _selectionModel.SelectedSessionPlace = _sessionPlace;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            SessionHeaderViewModel that = obj as SessionHeaderViewModel;
            if (that == null)
                return false;
            return Object.Equals(this._sessionPlace, that._sessionPlace);
        }

        public override int GetHashCode()
        {
            return _sessionPlace.GetHashCode();
        }
    }
}
