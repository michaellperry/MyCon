using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.ViewModels.MySchedule;
using FacetedWorlds.MyCon.ViewModels.Tracks;
using UpdateControls.XAML;

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

        public object Tracks
        {
            get
            {
                return ViewModel(delegate()
                {
                    return new TracksViewModel(_synchronizationService);
                });
            }
        }
    }
}
