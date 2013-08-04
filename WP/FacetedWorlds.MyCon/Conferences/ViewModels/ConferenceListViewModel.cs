using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.Conferences.Models;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceListViewModel
    {
        private readonly Catalog _catalog;
        private readonly ConferenceSelection _selection;
        private readonly Func<ConferenceHeader, ConferenceHeaderViewModel> _makeConferenceHeaderViewModel;

        public event Action<ConferenceHeader> ConferenceSelected;

        public ConferenceListViewModel(Catalog catalog, ConferenceSelection selection, Func<ConferenceHeader, ConferenceHeaderViewModel> makeConferenceHeaderViewModel)
        {
            _catalog = catalog;
            _selection = selection;
            _makeConferenceHeaderViewModel = makeConferenceHeaderViewModel;
        }

        public IEnumerable<ConferenceHeaderViewModel> Conferences
        {
            get
            {
                return
                    from conferenceHeader in _catalog.ConferenceHeaders
                    where conferenceHeader.StartDate.Candidates.Any()
                    orderby conferenceHeader.StartDate.Value
                    select _makeConferenceHeaderViewModel(conferenceHeader);
            }
        }

        public ConferenceHeaderViewModel SelectedConference
        {
            get
            {
                if (_selection.SelectedConference == null)
                    return null;

                return _makeConferenceHeaderViewModel(_selection.SelectedConference);
            }
            set
            {
                if (value == null)
                    _selection.SelectedConference = null;
                else
                {
                    _selection.SelectedConference = value.ConferenceHeader;

                    if (ConferenceSelected != null)
                        ConferenceSelected(value.ConferenceHeader);
                }
            }
        }
    }
}
