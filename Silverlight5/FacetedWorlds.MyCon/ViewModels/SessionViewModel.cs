using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionViewModel
    {
        private readonly Session _session;

        public SessionViewModel(Session session)
        {
            _session = session;
        }

        internal Session Session
        {
            get { return _session; }
        }

        public string Name
        {
            get { return _session.Name; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            SessionViewModel that = obj as SessionViewModel;
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
