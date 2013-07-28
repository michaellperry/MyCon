using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();
        }

        public object Main
        {
            get
            {
                return ViewModel(() => _synchronizationService.Individual == null
                    ? null :
                    new MainViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Individual));
            }
        }

        public object ConferenceList
        {
            get
            {
                return ViewModel(() =>
                    ConferenceListViewModelFactory.Create(_synchronizationService.Community));
            }
        }
    }
}
