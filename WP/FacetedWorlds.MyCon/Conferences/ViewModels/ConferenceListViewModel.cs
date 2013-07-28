using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedWorlds.MyCon.Conferences.ViewModels
{
    public class ConferenceListViewModel
    {
        public IEnumerable<ConferenceHeaderViewModel> Conferences
        {
            get
            {
                return new List<ConferenceHeaderViewModel>
                {
                    new ConferenceHeaderViewModel
                    {
                        Name = "Cowtown Code Camp 2013",
                        ImageUrl = "http://cdn.marketplaceimages.windowsphone.com/v8/images/2a24b5e4-d79a-448e-8818-4040ff0d51a6?imageType=ws_icon_large",
                        Date = new DateTime(2013, 3, 17),
                        Location = "Fort Worth, TX"
                    },
                    new ConferenceHeaderViewModel
                    {
                        Name = "AwesomeFest 2014",
                        ImageUrl = "http://files.g4tv.com/ImageDb3/284724_S/darth-vader-rock-guitarist.jpg",
                        Date = new DateTime(2014, 2, 27),
                        Location = "Allen, TX"
                    },
                    new ConferenceHeaderViewModel
                    {
                        Name = "That Conference 2013",
                        ImageUrl = "http://www.thatconference.com/Images/mainLogo.png",
                        Date = new DateTime(2013, 8, 16),
                        Location = "Wisconsin Dells, WI"
                    },
                };
            }
        }
    }
}
