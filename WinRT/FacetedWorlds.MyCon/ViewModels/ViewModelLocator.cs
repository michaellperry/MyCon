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
                return ViewModel(() => _synchronizationService.Identity == null
                    ? null :
                    new MainViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Identity));
            }
        }

        public object Schedule
        {
            get
            {
                return ViewModel(() => _synchronizationService.Attendee == null
                    ? null
                    : new ScheduleViewModel(
                        _synchronizationService,
                        _synchronizationService.Attendee,
                        _searchModel)
                );
            }
        }
    }
}
