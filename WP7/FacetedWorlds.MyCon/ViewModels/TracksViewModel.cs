using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Models;
using UpdateControls.Collections;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class TracksViewModel
    {
        private readonly Attendee _attendee;
        private readonly ImageCache _imageCache;
        private readonly SearchModel _searchModel;

        private DependentList<string> _trackNames;

        public TracksViewModel(Attendee attendee, ImageCache imageCache, SearchModel searchModel)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _searchModel = searchModel;

            _trackNames = new DependentList<string>(() =>
                from track in _attendee.Conference.Tracks
                orderby track.Name
                select track.Name);
        }

        public int SelectedTrackIndex
        {
            get
            {
                int index = _trackNames
                    .ToList()
                    .IndexOf(_searchModel.SelectedTrack);
                return index == -1 ? 0 : index;
            }
            set
            {
                _searchModel.SelectedTrack = _trackNames.ElementAtOrDefault(value);
            }
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _attendee.Conference.Tracks
                    orderby track.Name
                    select new TrackViewModel(_attendee, track, _imageCache);
            }
        }
    }
}
