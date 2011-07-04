using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using $rootnamespace$.Models;
using UpdateControls.Correspondence;
using UpdateControls.XAML;

namespace $rootnamespace$.ViewModels
{
    public class MainViewModel
    {
        private Community _community;
        private NavigationModel _navigationModel;
        private SynchronizationService _synhronizationService;

        public MainViewModel(Community community, NavigationModel navigationModel, SynchronizationService synhronizationService)
        {
            _community = community;
            _navigationModel = navigationModel;
            _synhronizationService = synhronizationService;
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }
    }
}
