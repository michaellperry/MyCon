using System;
using System.Runtime.CompilerServices;
using FacetedWorlds.MyCon.Models;
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

        public object Main
        {
            get { return ViewModel(() => new MainViewModel(_synchronizationService, _searchModel)); }
        }

        public object MySchedule
        {
            get
            {
                return ViewModel(() =>
                {
                    var conference = _synchronizationService.Conference;
                    var individual = _synchronizationService.Individual;
                    if (conference == null ||
                        individual == null)
                        return null;

                    return ViewModels.MySchedule.Container.CreateViewModel(
                        conference,
                        individual,
                        _selectionModel);
                });
            }
        }

        public object AvailableSessions
        {
            get
            {
                return ViewModel(() =>
                {
                    var time = _selectionModel.SelectedTime;
                    var individual = _synchronizationService.Individual;

                    if (time == null ||
                        individual == null)
                        return null;

                    return ViewModels.AvailableSessions.Container.CreateViewModel(
                        time,
                        individual,
                        _selectionModel);
                });
            }
        }

        public object AllSessions
        {
            get
            {
                return ViewModel(() =>
                {
                    return ViewModels.AllSessions.Container.CreateViewModel(
                        _selectionModel,
                        _searchModel,
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
                    var sessionPlace = _selectionModel.SelectedSessionPlace;
                    var individual = _synchronizationService.Individual;
                    if (sessionPlace == null ||
                        individual == null)
                        return null;

                    return new SessionViewModel(sessionPlace, individual);
                });
            }
        }
    }
}
