using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Conferences.Models;
using FacetedWorlds.MyCon.Conferences.ViewModels;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly ConferenceSelection _conferenceSelection;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();

            _conferenceSelection = new ConferenceSelection();
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
                    ConferenceListViewModelFactory.Create(
                        _synchronizationService.Community,
                        _conferenceSelection));
            }
        }

        public object ConferenceDetails
        {
            get
            {
                return ViewModel(delegate()
                {
                    if (_conferenceSelection.SelectedConference == null)
                        return null;

                    return new ConferenceDetailsViewModel(
                        _conferenceSelection.SelectedConference,
                        _conferenceSelection);
                });
            }
        }
    }
}
