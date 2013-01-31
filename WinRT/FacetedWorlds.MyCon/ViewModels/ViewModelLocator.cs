using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
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
    }
}
