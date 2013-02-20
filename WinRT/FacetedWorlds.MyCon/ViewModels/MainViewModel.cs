using System;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        public enum ViewOption
        {
            MyScheduleView,
            AllSessionsView
        };

        private Independent<ViewOption> _selectedView = new Independent<ViewOption>();

        public ViewOption SelectedView
        {
            get { return _selectedView; }
            set { _selectedView.Value = value; }
        }
    }
}
