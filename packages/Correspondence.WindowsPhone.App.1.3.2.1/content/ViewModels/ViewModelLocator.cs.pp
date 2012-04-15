using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;

namespace $rootnamespace$.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;
        private readonly SettingsViewModel _settings;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();
            _main = new MainViewModel(_synchronizationService.Identity, _synchronizationService);
            _settings = new SettingsViewModel(_synchronizationService.Identity);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Settings
        {
            get { return ForView.Wrap(_settings); }
        }
    }
}
