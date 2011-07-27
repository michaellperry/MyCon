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

        private DependentList<Track> _tracks;

        public TracksViewModel(Attendee attendee, ImageCache imageCache, SearchModel searchModel)
        {
            _attendee = attendee;
            _imageCache = imageCache;
            _searchModel = searchModel;

            _tracks = new DependentList<Track>(() =>
                from track in _attendee.Conference.Tracks
                orderby track.Name
                select track);
        }

        public int SelectedTrackIndex
        {
            get
            {
                int index = _tracks
                    .Select(track => track.Name)
                    .ToList()
                    .IndexOf(_searchModel.SelectedTrack);
                return index == -1 ? 0 : index;
            }
            set
            {
                _searchModel.SelectedTrack = _tracks.Select(track => track.Name).ElementAtOrDefault(value);
            }
        }

        public IEnumerable<TrackViewModel> Tracks
        {
            get
            {
                return
                    from track in _tracks
                    orderby track.Name
                    select new TrackViewModel(_attendee, track, _imageCache);
            }
        }
    }
}
