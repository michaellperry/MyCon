using System;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public struct CachedImage
    {
        public string ImageUrl;
        public Action Access;
    }
}
