using System;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private Identity _identity;
        private SynchronizationService _synhronizationService;

        public MainViewModel(Identity identity, SynchronizationService synhronizationService)
        {
            _identity = identity;
            _synhronizationService = synhronizationService;
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }

        public ScheduleViewModel Schedule
        {
            get { return new ScheduleViewModel(_identity.NewAttendee("Conference ID")); }
        }
    }
}
