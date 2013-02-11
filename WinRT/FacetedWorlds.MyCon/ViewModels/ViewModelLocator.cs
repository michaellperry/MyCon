using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.Models;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService =
            new SynchronizationService();
        private readonly SearchModel _searchModel =
            new SearchModel();

        public ViewModelLocator()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _synchronizationService.Initialize();
        }

        public object Main
        {
            get
            {
                return ViewModel(() => _synchronizationService.Individual == null
                    ? null :
                    new MainViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Individual));
            }
        }

        public object Schedule
        {
            get
            {
                return ViewModel(() =>
                {
                    var conference = _synchronizationService.Conference;
                    var individual = _synchronizationService.Individual;
                    if (conference == null &&
                        individual == null)
                        return null;

                    return new ScheduleViewModel(
                        _synchronizationService,
                        conference,
                        individual,
                        _searchModel);
                });
            }
        }

        public SampleData.SampleDataSource _source = new SampleData.SampleDataSource();
        public object Tracks
        {
            get
            {
//                return _source;
                return ViewModel(delegate()
                {
                    //if (_synchronizationService.Conference == null)
                    //    return null;
                    //if (!_synchronizationService.Conference.Tracks.Any())
                    //    return null;

                    return new TracksViewModel(_synchronizationService);
                });
            }
        }
    }
}
