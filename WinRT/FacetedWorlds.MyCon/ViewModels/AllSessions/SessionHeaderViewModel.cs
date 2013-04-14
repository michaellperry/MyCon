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
            get { return _sessionPlace.Session.Name; }
        }

        public string Subtitle
        {
            get { return _sessionPlace.Session.Speaker.Name; }
        }

        public ImageSource Image
        {
            get
            {
                string url = _sessionPlace.Session.Speaker.ImageUrl.Value;
                if (String.IsNullOrEmpty(url))
                    return null;

                return new BitmapImage(new Uri(url, UriKind.Absolute));
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
