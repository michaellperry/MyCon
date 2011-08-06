using System;
using System.ComponentModel;
using System.Linq;
using FacetedWorlds.MyCon.Models;
using UpdateControls.XAML;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class ViewModelLocator
    {
        private readonly NavigationModel _navigationModel;
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            _navigationModel = new NavigationModel();
            _synchronizationService = new SynchronizationService(_navigationModel);
            if (!DesignerProperties.IsInDesignTool)
            {
                _synchronizationService.Initialize();
                TemporarilyPreselectDallasTechFest();
            }
            _main = new MainViewModel(_synchronizationService.Community, _navigationModel, _synchronizationService);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }

        public object Schedule
        {
            get { return ForView.Wrap(new ScheduleViewModel(_navigationModel.SelectedConference, _navigationModel)); }
        }

        private void TemporarilyPreselectDallasTechFest()
        {
            _navigationModel.SelectedConference = _synchronizationService.Conference;
        }
    }
}
