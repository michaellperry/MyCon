using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;
using FacetedWorlds.MyCon.SampleData;

namespace FacetedWorlds.MyCon.ViewModels.Tracks
{
    public class TrackViewModel
    {
        private readonly Track _track;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public IEnumerable<SampleDataItem> Items
        {
            get
            {
                String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                            "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");
                SampleDataTrack group1 = null;

                return new List<SampleDataItem>
                {
                    new SampleDataItem("Group-1-Item-1",
                        "Item Title: 1",
                        "Item Subtitle: 1",
                        "Assets/LightGray.png",
                        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                        ITEM_CONTENT,
                        group1),
                    new SampleDataItem("Group-1-Item-2",
                        "Item Title: 2",
                        "Item Subtitle: 2",
                        "Assets/DarkGray.png",
                        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                        ITEM_CONTENT,
                        group1),
                    new SampleDataItem("Group-1-Item-3",
                        "Item Title: 3",
                        "Item Subtitle: 3",
                        "Assets/MediumGray.png",
                        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                        ITEM_CONTENT,
                        group1),
                    new SampleDataItem("Group-1-Item-4",
                        "Item Title: 4",
                        "Item Subtitle: 4",
                        "Assets/DarkGray.png",
                        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                        ITEM_CONTENT,
                        group1),
                    new SampleDataItem("Group-1-Item-5",
                        "Item Title: 5",
                        "Item Subtitle: 5",
                        "Assets/MediumGray.png",
                        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                        ITEM_CONTENT,
                        group1),
                };
            }
        }
    }
}
