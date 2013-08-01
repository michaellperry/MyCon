using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceHeaderViewModel
    {
        private readonly ConferenceHeader _conferenceHeader;

        public ConferenceHeaderViewModel(ConferenceHeader conferenceHeader)
        {
            _conferenceHeader = conferenceHeader;
        }

        public ConferenceHeader ConferenceHeader
        {
            get { return _conferenceHeader; }
        }

        public string Name
        {
            get { return _conferenceHeader.Name; }
        }

        public string ImageUrl
        {
            get { return _conferenceHeader.ImageUrl; }
        }

        public string Date
        {
            get
            {
                DateTime start = _conferenceHeader.StartDate.Value;
                DateTime end = _conferenceHeader.EndDate.Value;
                if (end == start)
                    return String.Format("{0:MMMM d, yyyy}", start);
                else if (end.Year != start.Year)
                    return String.Format("{0:MMMM d, yyyy} - {1:MMMM d, yyyy}", start, end);
                else if (end.Month != start.Month)
                    return String.Format("{0:MMMM d} - {1:MMMM d, yyyy}", start, end);
                else
                    return String.Format("{0:MMMM d} - {1:d, yyyy}", start, end);
            }
        }

        public string Location
        {
            get { return _conferenceHeader.Location; }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            ConferenceHeaderViewModel that = obj as ConferenceHeaderViewModel;
            if (that == null)
                return false;
            return Object.Equals(this._conferenceHeader, that._conferenceHeader);
        }

        public override int GetHashCode()
        {
            return _conferenceHeader.GetHashCode();
        }
    }
}
