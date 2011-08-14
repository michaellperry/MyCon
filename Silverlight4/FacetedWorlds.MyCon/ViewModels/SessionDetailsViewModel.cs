using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using System.Windows.Input;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionDetailsViewModel
    {
        private readonly Session _session;

        public SessionDetailsViewModel(Session session)
        {
            _session = session;
        }

        public string Speaker
        {
            get { return _session.Speaker.Name; }
        }

        public string Track
        {
            get { return _session.Track.Name; }
        }

        public string Name
        {
            get { return _session.Name; }
            set { _session.Name = value; }
        }

        public string Description
        {
            get { return _session.Description.Value.JoinSegments(); }
            set { _session.SetDescription(value); }
        }

        public ICommand Delete
        {
            get
            {
                return MakeCommand
                    .When(() => !_session.SessionDeletes.Any())
                    .Do(() =>
                    {
                        _session.Delete();
                    });
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            SessionDetailsViewModel that = obj as SessionDetailsViewModel;
            if (that == null)
                return false;
            return _session.Equals(that._session);
        }

        public override int GetHashCode()
        {
            return _session.GetHashCode();
        }
    }
}
