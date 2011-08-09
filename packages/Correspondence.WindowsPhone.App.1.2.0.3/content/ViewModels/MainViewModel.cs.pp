using System;
using System.Linq;
using $rootnamespace$.Models;

namespace $rootnamespace$.ViewModels
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
    }
}
