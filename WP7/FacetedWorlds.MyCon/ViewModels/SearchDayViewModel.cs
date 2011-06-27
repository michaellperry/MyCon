using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.Collections;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SearchDayViewModel
    {
        private readonly Attendee _attendee;
        private readonly Day _day;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        private DependentList<SessionPlace> _matchingSessionPlaces;

        public SearchDayViewModel(Attendee attendee, Day day, ImageCache imageCache, SearchModel searchModel)
        {
            _attendee = attendee;
            _day = day;
            _imageCache = imageCache;
            _searchModel = searchModel;

            _matchingSessionPlaces = new DependentList<SessionPlace>(() =>
                _searchModel.SearchTerm == null || _searchModel.SearchTerm.Length < 3 ?
                    Enumerable.Empty<SessionPlace>() :
                    from time in _day.Times
                    from sessionPlace in time.AvailableSessions
                    where sessionPlace.Session.Matches(_searchModel.SearchTerm.ToLower())
                    select sessionPlace);
        }

        public string Day
        {
            get { return String.Format("{0:dddd, MMMM d}", _day.ConferenceDate.Date); }
        }

        public Visibility DayVisible
        {
            get { return _matchingSessionPlaces.Any() ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IEnumerable<TrackSessionViewModel> Results
        {
            get
            {
                return
                    from sessionPlace in _matchingSessionPlaces
                    orderby sessionPlace.Place.PlaceTime.Start
                    select new TrackSessionViewModel(
                        _attendee.NewSlot(sessionPlace.Place.PlaceTime),
                        sessionPlace,
                        _imageCache);
            }
        }
    }
}
