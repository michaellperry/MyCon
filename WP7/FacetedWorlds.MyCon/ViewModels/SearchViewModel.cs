using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SearchViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        public SearchViewModel(Attendee attendee, ImageCache imageCache, SearchModel searchModel)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _searchModel = searchModel;
        }

        public string SearchTerm
        {
            get { return _searchModel.SearchTerm; }
            set { _searchModel.SearchTerm = value; }
        }

        public IEnumerable<SearchDayViewModel> Days
        {
            get
            {
                return
                    from day in _attendee.Conference.Days
                    orderby day.ConferenceDate
                    select new SearchDayViewModel(_attendee, day, _imageCache, _searchModel);
            }
        }

        public Visibility NoResults
        {
            get
            {
                bool hasSearch = _searchModel.SearchTerm != null && _searchModel.SearchTerm.Length >= 3;
                if (!hasSearch)
                    return Visibility.Collapsed;

                bool hasResults = _attendee.Conference.Sessions.Any(session =>
                    session.Matches(_searchModel.SearchTerm.ToLower()));
                return hasResults ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
