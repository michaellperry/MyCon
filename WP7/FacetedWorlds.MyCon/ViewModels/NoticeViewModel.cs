using System;
using FacetedWorlds.MyCon.ImageUtilities;

namespace FacetedWorlds.MyCon.ViewModels
{
    public abstract class NoticeViewModel
    {
        public abstract CachedImage ImageUrl { get; }
        public abstract string Title { get; }
        public abstract string Text { get; }
        public abstract string Age { get; }

        protected static string ToAgeText(int minutesAgo)
        {
            if (minutesAgo < 1)
                return "Just now";
            if (minutesAgo < 2)
                return "A minute ago";
            if (minutesAgo < 60)
                return String.Format("{0} minutes ago", minutesAgo);
            if (minutesAgo < 120)
                return "An hour ago";
            if (minutesAgo < 60 * 24)
                return String.Format("{0} hours ago", minutesAgo / 60);
            return "A while back";
        }
    }
}
