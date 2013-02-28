using System;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.ViewModels.AvailableSessions
{
    public class SessionHeaderViewModel
    {
        private readonly SessionPlace _sessionPlace;
        private readonly Individual _individual;
        private readonly SelectionModel _selectionModel;
        private readonly Action _showSession;
        
        public SessionHeaderViewModel(SessionPlace sessionPlace, Individual individual, SelectionModel selectionModel, Action showSession)
        {
            _sessionPlace = sessionPlace;
            _individual = individual;
            _selectionModel = selectionModel;
            _showSession = showSession;
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

                return "Room: " + _sessionPlace.Place.Room.RoomNumber.Value;
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

        public Brush StatusBrush
        {
            get
            {
                string status;
                if (_individual.IsScheduled(_sessionPlace))
                    status = "ScheduledStatusBrush";
                else
                    status = "UnscheduledStatusBrush";
                return Application.Current.Resources[status] as Brush;
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
                        _showSession();
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
