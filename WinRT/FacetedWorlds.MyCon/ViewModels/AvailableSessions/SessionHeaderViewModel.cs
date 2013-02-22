using System;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.AvailableSessions
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

        public string Title
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

        public string Room
        {
            get
            {
                if (_sessionPlace.Place == null ||
                    _sessionPlace.Place.Room == null ||
                    _sessionPlace.Place.Room.RoomNumber.Value == null)
                    return null;

                return "Room " + _sessionPlace.Place.Room.RoomNumber.Value;
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

        public ICommand SelectSession
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        _selectionModel.SelectedSessionPlace = _sessionPlace;
                    });
            }
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
