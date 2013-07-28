using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceListViewModel
    {
        private readonly Catalog _catalog;
        private readonly Func<ConferenceHeader, ConferenceHeaderViewModel> _makeConferenceHeaderViewModel;

        public ConferenceListViewModel(Catalog catalog, Func<ConferenceHeader, ConferenceHeaderViewModel> makeConferenceHeaderViewModel)
        {
            _catalog = catalog;
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
    }
}
