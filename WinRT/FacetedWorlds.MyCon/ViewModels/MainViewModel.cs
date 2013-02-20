using System;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private readonly SynchronizationService _synchronizationService;

        public enum ViewOption
        {
            MyScheduleView,
            AllSessionsView
        };

        private Independent<ViewOption> _selectedView = new Independent<ViewOption>(ViewOption.AllSessionsView);

        public MainViewModel(SynchronizationService synchronizationService)
        {
            _synchronizationService = synchronizationService;
        }

        public ViewOption SelectedView
        {
            get { return _selectedView; }
            set { _selectedView.Value = value; }
        }

        public bool Synchronizing
        {
            get { return _synchronizationService.Community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                Exception exception = _synchronizationService.Community.LastException;
                return exception == null ? null : exception.Message;
            }
        }

        public string Conference
        {
            get
            {
                if (_synchronizationService.Conference == null)
                    return null;

                return _synchronizationService.Conference.Name;
            }
        }
    }
}
