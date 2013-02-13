using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.ViewModels.MySchedule;
using FacetedWorlds.MyCon.ViewModels.Session;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService =
            new SynchronizationService();
        private readonly SearchModel _searchModel =
            new SearchModel();
        private readonly SelectionModel _selectionModel =
            new SelectionModel();

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
                return ViewModel(() =>
                {
                    return ViewModels.Tracks.Container.CreateViewModel(
                        _selectionModel,
                        _synchronizationService);
                });
            }
        }

        public object Session
        {
            get
            {
                return ViewModel(() =>
                {
                    if (_selectionModel.SelectedSessionPlace == null)
                        return null;

                    return new SessionViewModel(_selectionModel.SelectedSessionPlace);
                });
            }
        }
    }
}
