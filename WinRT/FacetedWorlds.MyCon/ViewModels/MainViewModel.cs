using System;
using FacetedWorlds.MyCon.Models;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly SearchModel _search;

        public enum ViewOption
        {
            MyScheduleView,
            AllSessionsView
        };

        private Independent<ViewOption> _selectedView = new Independent<ViewOption>(ViewOption.MyScheduleView);

        public MainViewModel(SynchronizationService synchronizationService, SearchModel search)
        {
            _synchronizationService = synchronizationService;
            _search = search;
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

        public void PerformSearch(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                _search.SearchTerm = searchTerm;
                _selectedView.Value = ViewOption.AllSessionsView;
            }
        }

        public void ClearSearch()
        {
            _search.SearchTerm = null;
        }
    }
}
