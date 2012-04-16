using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using $rootnamespace$.Models;
using UpdateControls.XAML;

namespace $rootnamespace$.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            NavigationModel navigationModel = new NavigationModel();
            _synchronizationService = new SynchronizationService(navigationModel);
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                _synchronizationService.Initialize();
            _main = new MainViewModel(_synchronizationService.Community, navigationModel, _synchronizationService);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }
    }
}
