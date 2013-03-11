using System;
using System.Windows.Input;
using FacetedWorlds.MyCon.Models;
using UpdateControls.Fields;
using UpdateControls.XAML;
using Windows.UI.Xaml;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly SearchModel _search;
        private readonly SelectionModel _selection;

        public enum ViewOption
        {
            MyScheduleView,
            AllSessionsView
        };

        private Independent<ViewOption> _selectedView = new Independent<ViewOption>(ViewOption.MyScheduleView);

        public MainViewModel(
            SynchronizationService synchronizationService,
            SearchModel search,
            SelectionModel selection)
        {
            _synchronizationService = synchronizationService;
            _search = search;
            _selection = selection;
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

        public Visibility VisibleWhenNoSelection
        {
            get
            {
                return _selection.SelectedTime == null
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility VisibleWhenSelection
        {
            get
            {
                return _selection.SelectedTime == null
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        public ICommand ClearSelection
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _selection.SelectedTime = null;
                    });
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
